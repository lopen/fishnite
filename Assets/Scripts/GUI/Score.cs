using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;

    private float score = 0f;
    [SerializeField] private TextMeshProUGUI score1;

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

        ResetScore();
    }

    public void IncreaseScore() {
        score++;
        UpdateScore();
    }

    public void IncreaseScoreBy(float amount) {
        score = score + amount;
        UpdateScore();
    }

    public void DecreaseScore() {
        score--;
        UpdateScore();
    }

    public void DecreaseScoreBy(float amount) {
        score = score - amount;
        UpdateScore();
    }

    private void ResetScore() {
        score = 0;
        UpdateScore();
    }

    private void UpdateScore() {
        score1.text = score.ToString("0000");
    }

    public float GetScore() {
        return score;
    }
}
