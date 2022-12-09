using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    
    public static Timer instance;

    public float timeDuration = 3f * 60f;
    private float timer;

    private float flashTimer;
    private float flashDuration = 1f;
    public string currentTime;

    [SerializeField] private TextMeshProUGUI min1;
    [SerializeField] private TextMeshProUGUI min2;
    [SerializeField] private TextMeshProUGUI seperator;
    [SerializeField] private TextMeshProUGUI sec1;
    [SerializeField] private TextMeshProUGUI sec2;

    void Start() {

        if (instance == null)
        {
            instance = this;
        } 
        else if (instance != this) 
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        ResetTimer();
    }

    void Update() {
        if(timer > 0) {
            timer -= Time.deltaTime;
            UpdateDisplay(timer);
        } else {
            Flash();
        }
    }

    public void BoostTime() {
        timer += 20;
    }

    public void ReduceTime() {
        timer += 20;
    }

    private void ResetTimer() {
        timer = timeDuration;
    }

    private void UpdateDisplay(float time) {
        float mins = Mathf.FloorToInt(time / 60);
        float secs = Mathf.FloorToInt(time % 60);

        currentTime = string.Format("{00:00}{1:00}", mins, secs);
        min1.text = currentTime[0].ToString();
        min2.text = currentTime[1].ToString();

        sec1.text = currentTime[2].ToString();
        sec2.text = currentTime[3].ToString();
    }

    private void Flash() {
        if (timer != 0) {
            timer = 0;
            UpdateDisplay(timer);
        }

        if(flashTimer <= 0) {
            flashTimer = flashDuration;
        } else if (flashTimer >= flashDuration / 2) {
            flashTimer -= Time.deltaTime;
            SetDisplay(false);
        } else {
            flashTimer -= Time.deltaTime;
            SetDisplay(true);
        }
    }

    private void SetDisplay(bool status) {
        min1.enabled = status;
        min2.enabled = status;

        seperator.enabled = status;

        sec1.enabled = status;
        sec2.enabled = status;
    }

    public string GetCurrentTime() {
        return currentTime;
    }
}
