using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance; // Timer instance

    public float timeDuration = 2f * 60f; // Amount of timer time
    private float timer; // Timer amount

    private float flashTimer; // Flash for when time is up
    private float flashDuration = 1f; // Time-up flash speed
    public string currentTime; // String for formatting time

    // Timer is laid out in digital format
    [SerializeField] private TextMeshProUGUI min1; // Reference to minute 1
    [SerializeField] private TextMeshProUGUI min2; // Reference to minute 2
    [SerializeField] private TextMeshProUGUI seperator; // Reference to seperator
    [SerializeField] private TextMeshProUGUI sec1; // Reference to second 1
    [SerializeField] private TextMeshProUGUI sec2; // Reference to second 2

    // Start is called before the first frame update / Handles instance checking for timer
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

    // Update is called once per frame / Stores current time value
    void Update() {
        if(timer > 0) {
            timer -= Time.deltaTime;
            UpdateDisplay(timer);
        } else {
            Flash();
        }
    }

    // Boost time method, used by powerups
    public void BoostTime() {
        timer += 20;
    }

    // Reduce time method, used by powerups
    public void ReduceTime() {
        timer += 20;
    }

    // Reset timer, used on level end
    private void ResetTimer() {
        timer = timeDuration;
    }

    // Update display method / Formats current time into a digital format
    private void UpdateDisplay(float time) {
        float mins = Mathf.FloorToInt(time / 60);
        float secs = Mathf.FloorToInt(time % 60);

        currentTime = string.Format("{00:00}{1:00}", mins, secs);
        min1.text = currentTime[0].ToString();
        min2.text = currentTime[1].ToString();

        sec1.text = currentTime[2].ToString();
        sec2.text = currentTime[3].ToString();
    }

    // Flash method / When time is up, enables flashing of clock
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

    // Method for setting display status
    private void SetDisplay(bool status) {
        min1.enabled = status;
        min2.enabled = status;

        seperator.enabled = status;

        sec1.enabled = status;
        sec2.enabled = status;
    }

    // Method for returning current time
    public string GetCurrentTime() {
        return currentTime;
    }
}
