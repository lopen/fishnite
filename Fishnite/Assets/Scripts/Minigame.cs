using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{
    private Text thisText;
    public GameObject camera;
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
