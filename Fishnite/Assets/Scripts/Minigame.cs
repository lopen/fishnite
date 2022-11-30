using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{
    // Event handler for minigame starting, for enemies to stop chasing & timer to stop running
    public delegate void MinigameStart();
    public static event MinigameStart MinigameTrigger;

    private Text thisText;
    //public GameObject camera;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Text>();
        thisText = GetComponent<Text>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.P))
       {
            score += 500;
       } 
       thisText.text = "Score is " + score; 
    }
}
