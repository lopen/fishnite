using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    
    private float score = 0f;
    [SerializeField] private TextMeshProUGUI score1;

    void Start() {
        ResetTimer();
    }

    void Update() {
        UpdateScore();
    }

    public void IncreaseScore() {
        score++;
    }

    public void DecreaseScore() {
        score--;
    }

    private void ResetTimer() {
        score = 0;
    }

    private void UpdateScore() {
        score1.text = score.ToString();
    }
}
