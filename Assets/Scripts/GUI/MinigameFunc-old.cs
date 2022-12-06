using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;

public class MinigameFuncold : MonoBehaviour
{
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
    
    private float sliderval;
    private float sliderSpeed; 

    private float sliderMin;
    private float sliderMax;
    private float triggerTest;

    private bool userClick;
    private bool runSlider;

    private int raiseCount = 0;
    private int failCount = 0;

    // Event handling

    public delegate void StartMinigame();
    public delegate void FailMinigame();
    public delegate void WinMinigame();

    public static event StartMinigame OnStart;
    public static event FailMinigame OnFailed;
    public static event WinMinigame OnWon;

    void Start() {
        runSlider = true;
        StartCoroutine(UpdateSliderVal());
        GenerateTrigger();
        Time.timeScale = 0;
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
    }

    // Co-routine for increasing slider value, update handles this too fast and there is no need to call this following completion.
    IEnumerator UpdateSliderVal () {
        while (runSlider == true) {

            if (sliderUI.value == 0) {
                sliderSpeed = 2f;
            } else if (sliderUI.value == 100) {
                sliderSpeed = -2f;
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
            
            //sliderSpeed += sliderSpeed * Time.unscaledDeltaTime;
            sliderUI.value = sliderSpeed + sliderUI.value;
            sliderval = sliderUI.value;
            sliderval += -70 + sliderSpeed + sliderUI.value / 2.5f;

            triggerObject.GetComponent<RectTransform>().localPosition = new Vector3(sliderval,0,0);
            yield return new WaitForSecondsRealtime(0.5f * Time.deltaTime);
        }
    }

    IEnumerator TimeWaitDestroy (float time) {
        yield return new WaitForSecondsRealtime(time);
        Destroy(minigameRef);
        Time.timeScale = 1;
    }

    private void ProgressStage() {
        sliderUI.transform.Translate(0f, 60f*Time.unscaledDeltaTime, 0f);
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

            StartCoroutine(TimeWaitDestroy(3f));
        }
        print(raiseCount);
    }

    private void DegressStage() {
        if (raiseCount > 0) {
            sliderUI.transform.Translate(0f, -60f*Time.unscaledDeltaTime, 0f);
            raiseCount--;
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
        print(raiseCount);
    }

    private void GenerateTrigger() {
        Destroy(trigger);
        trigger = Instantiate(triggerZone, triggerContainer) as GameObject;
        trigger.transform.localPosition = new Vector3(Random.Range(-70, 70), 0, 0);
    }
}