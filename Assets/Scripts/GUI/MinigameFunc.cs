using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Random=UnityEngine.Random;

public class MinigameFunc : MonoBehaviour
{

    public delegate void MinigameStart(); // Event delegation for minigame being live, referenced in inventory
    public static event MinigameStart MinigameLaunch; // Event for minigame being live, referenced in inventory

    public delegate void MinigameEnd(); // Event delegation for minigame being finished, referenced in inventory
    public static event MinigameEnd MinigameFinish; // Event for minigame being finished, referenced in inventory

    [SerializeField] private GameObject minigameRef; // Self reference for minigame, used for destroying following completion
    [SerializeField] public Slider sliderUI; // Self reference for value slider, used for providing functionality to the minigame / Slightly unorthodox approach, but works well
    [SerializeField] private GameObject sliderObject; // Self reference to slider object

    [SerializeField] public GameObject hook; // Self reference to slider value object, aka 'the hook'
    [SerializeField] public GameObject hookPoint; // Self reference to line connect point, placed in a sub-transform as to improve aesthetic
    [SerializeField] private Animator playerAnims; // Fisherman player animations, found atop the minigame
    [SerializeField] private GameObject fishingLine; // Reference to line renderer component used to tie between hookpoint and fisherman 'FishingPoint'

    [SerializeField] private GameObject triggerZone; // Zone of trigger for minigame success
    [SerializeField] private GameObject triggerObject; // Object for trigger, uses a sprite mask to stay behind main fish sprite
    [SerializeField] private Transform triggerContainer; // Container for holding trigger, bounds used as to generate within normal, expected ranges

    [SerializeField] private GameObject lossScreen; // Screen prompted when player loses the minigame
    [SerializeField] private GameObject winScreen; // Screen prompted when player WINS the minigame

    private GameObject trigger; // Specific instantiated trigger, regenerated on success/fail of minigame
    private float upDown = 0f; // Value used to determine direction of movement - translate cannot be performed outside of an Update/deltaTime function
    
    private float sliderval; // Current value of slider
    private float sliderSpeed; // Current speed of slider value movement, progresses in speed when getting near the end of our trigger container, then resets

    private float sliderMin; // Slider minimum X boundary - colliders could not be used with UI, so we generated a custom transform check as to determine when in the trigger zone
    private float sliderMax; // Slider maximum X boundary
    private float triggerTest; // Custom object used to trace speed of slider, used as a customer collider

    private bool userClick; // Status bool for on user click
    private bool runSlider; // Status bool for slider running

    private int raiseCount = 0; // Count for amount of successful hooks, determines success of minigame
    private int failCount = 0; // Count for amount of failed hooks, determines loss

    // Inventory handling
    private PlayerInv playerInventory;
    private bool fishmoving = false;

    // Fish objects, used to provide player with fish for sale/points exchange 
    public List<ItemData> fishes;
    private ItemData fish;
    

    // Start is called before the first frame update / Handles initialising of slider and timescale halt
    void Start() {
        playerInventory = Player.instance.GetComponent<PlayerInv>();
        fish = fishes[Random.Range(0, fishes.Count)];
         
        runSlider = true;
        StartCoroutine(UpdateSliderVal());
        GenerateTrigger();

        Time.timeScale = 0;
        MinigameLaunch.Invoke();
    }

    // Update is called once per frame / Handles useri nput checks, slider transform allocation and initial translate of slider/minigame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            userClick = true;
        } else if (Input.GetKeyUp("space")) {
            userClick = false;
        }
        sliderMin = trigger.GetComponent<RectTransform>().localPosition.x;
        sliderMax = sliderMin + 10.5f;

        triggerTest = triggerObject.GetComponent<RectTransform>().localPosition.x;
        if (fishmoving == true)
        {
            sliderUI.transform.Translate(0f, upDown * 0.2f * Time.unscaledDeltaTime, 0f);
        }
    }

    // Co-routine for increasing slider value, update handles this too fast and there is no need to call this following completion.
    IEnumerator UpdateSliderVal () {
        while (runSlider == true) {

            if (sliderUI.value == 0) {
                sliderSpeed = 0.2f;
            } else if (sliderUI.value == 100) {
                sliderSpeed = -0.2f;
            }

            if (triggerTest > sliderMin && triggerTest < sliderMax && userClick == true) {   
                runSlider = false;
                userClick = false;
                hook.SetActive(false);
                GenerateTrigger();
                ProgressStage();
            } else if (userClick == true && triggerTest > sliderMax || userClick == true && triggerTest < sliderMin) {
                runSlider = false;
                userClick = false;
                hook.SetActive(false);
                DegressStage();
            } 
            
            sliderSpeed += sliderSpeed * Time.unscaledDeltaTime;
            sliderUI.value = sliderSpeed + sliderUI.value;
            sliderval = sliderUI.value;
            sliderval += -70 + sliderSpeed + sliderUI.value / 2.5f;

            triggerObject.GetComponent<RectTransform>().localPosition = new Vector3(sliderval,0,0);
            yield return new WaitForSecondsRealtime(0.5f * Time.unscaledDeltaTime);
        }
    }

    // Time wait co-routine, used due to Time.timeScale being 0 during run of initial co-routine, needed for resuming timescale.
    IEnumerator TimeWaitDestroy (float time) {
        yield return new WaitForSecondsRealtime(time);
        Destroy(minigameRef);
        Time.timeScale = 1;
        MinigameFinish.Invoke();
    }

    // Called upon successful pass of minigame stage, sets animation, equivilent values and provides winning items
    private void ProgressStage() {
        fishmoving = true;
        upDown = 1;
        StartCoroutine(moveFish(1));
        hook.SetActive(true);
        raiseCount++;
        runSlider = true;
        sliderUI.value = 0f;
        if (raiseCount == 1) {
            runSlider = false;
            
            winScreen.SetActive(true);
            fishingLine.SetActive(false);
            playerAnims.SetBool("Winner", true);
            sliderObject.SetActive(false);
            playerInventory.AddItem(fish);

            StartCoroutine(TimeWaitDestroy(3f));
        }
    }

    // Called upon failed attempt of minigame - user has two chances to fail, on their second the minigame is cancelled.
    private void DegressStage() {
        if (raiseCount > 0) {
            raiseCount--;
        }

        if (failCount > 0) {
            fishmoving = true;
            upDown = -1;
            StartCoroutine(moveFish(1));
        }
        
        failCount++;
        hook.SetActive(true);

        if (failCount >= 2) {
            runSlider = false;
           
            lossScreen.SetActive(true);
            fishingLine.SetActive(false);
            playerAnims.SetBool("Failed", true);
            sliderObject.SetActive(false);

            StartCoroutine(TimeWaitDestroy(3f));
        } else if (failCount < 2) {
            runSlider = true;
        }
        sliderUI.value = 0f;
    }

    // Co-routine for moving fish position outside of timescale
    IEnumerator moveFish(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        fishmoving = false;
    }

    // Method for generating trigger between random positions in container
    private void GenerateTrigger() {
        Destroy(trigger);
        trigger = Instantiate(triggerZone, triggerContainer) as GameObject;
        trigger.transform.localPosition = new Vector3(Random.Range(-70, 70), 0, 0);
    }
}