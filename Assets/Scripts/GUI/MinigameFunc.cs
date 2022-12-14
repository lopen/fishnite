using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Random=UnityEngine.Random;

public class MinigameFunc : MonoBehaviour
{

    public delegate void MinigameStart();
    public static event MinigameStart MinigameLaunch;

    public delegate void MinigameEnd();
    public static event MinigameEnd MinigameFinish;

    [SerializeField] private GameObject minigameRef;
    [SerializeField] public Slider sliderUI;
    [SerializeField] private GameObject sliderObject;

    [SerializeField] public GameObject hook;
    [SerializeField] public GameObject hookPoint;
    [SerializeField] private Animator playerAnims;
    [SerializeField] private GameObject fishingLine;

    [SerializeField] private GameObject triggerZone;
    [SerializeField] private GameObject triggerObject;
    [SerializeField] private Transform triggerContainer;

    [SerializeField] private GameObject lossScreen;
    [SerializeField] private GameObject winScreen;

    private GameObject trigger;
    private bool fishmoving = false;
    private float upDown = 0f;
    
    private float sliderval;
    private float sliderSpeed; 

    private float sliderMin;
    private float sliderMax;
    private float triggerTest;

    private bool userClick;
    private bool runSlider;

    private int raiseCount = 0;
    private int failCount = 0;

    // Inventory handling
    private PlayerInv playerInventory;

    // Fish object
    public List<ItemData> fishes;
    private ItemData fish;


    void Start() {
        playerInventory = Player.instance.GetComponent<PlayerInv>();
        fish = fishes[Random.Range(0, fishes.Count)];
        
        // get image object
        // assigned fish.imagesprite

        runSlider = true;
        StartCoroutine(UpdateSliderVal());
        GenerateTrigger();
        Time.timeScale = 0;
        MinigameLaunch.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            userClick = true;
        } else if (Input.GetKeyUp("space")) {
            print("keyup");
            userClick = false;
        }
        sliderMin = trigger.GetComponent<RectTransform>().localPosition.x;
        sliderMax = sliderMin + 10.5f;

        triggerTest = triggerObject.GetComponent<RectTransform>().localPosition.x;
        if (fishmoving)
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

    IEnumerator TimeWaitDestroy (float time) {
        yield return new WaitForSecondsRealtime(time);
        Destroy(minigameRef);
        Time.timeScale = 1;
        MinigameFinish.Invoke();
    }

    private void ProgressStage() {
        //sliderUI.transform.Translate(0f, 60f*Time.unscaledDeltaTime, 0f);
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
        print(raiseCount);
    }

    private void DegressStage() {
        if (raiseCount > 0) {
            raiseCount--;
        }

        if (failCount > 0)
        {
            fishmoving = true;
            upDown = -1;
            StartCoroutine(moveFish(1));
        }
        failCount++;
        hook.SetActive(true);

        if (failCount >= 2) {
            runSlider = false;
            //OnFailed();
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
    IEnumerator moveFish(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        fishmoving = false;
    }

    private void GenerateTrigger() {
        Destroy(trigger);
        trigger = Instantiate(triggerZone, triggerContainer) as GameObject;
        trigger.transform.localPosition = new Vector3(Random.Range(-70, 70), 0, 0);
    }
}