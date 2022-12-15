using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance; // Current instance of score

    private float score = 0f; // Current score alue
    [SerializeField] private TextMeshProUGUI score1; // Score text item, stores score float

    // Start is called before the first frame update / Checks for instance of score
    void Start() {
        if (instance == null) {
            instance = this;
        } 
        else if (instance != this) {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
        ResetScore();
    }

    // Method for increasing score
    public void IncreaseScore() {
        score++;
        UpdateScore();
    }

    // Method for increasing score by specific float amount/value
    public void IncreaseScoreBy(float amount) {
        score = score + amount;
        UpdateScore();
    }

    // Method for decreasing score
    public void DecreaseScore() {
        score--;
        UpdateScore();
    }

    // Method for decreasing score by specific float amount/value
    public void DecreaseScoreBy(float amount) {
        score = score - amount;
        UpdateScore();
    }

    // Method for resetting score counter, used when game is reset etc
    private void ResetScore() {
        score = 0;
        UpdateScore();
    }

    // Method for taking current score float, formatting to 'arcade' style and storing in text element. Game score is stored by game handler in PlayerPrefs at end of game.
    private void UpdateScore() {
        score1.text = score.ToString("0000");
    }

    // Method for getting current score
    public float GetScore() {
        return score;
    }
}
