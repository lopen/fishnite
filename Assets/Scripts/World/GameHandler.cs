using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject ingameMusic;
    [SerializeField] public GameObject timer;
    [SerializeField] public GameObject player;

    public bool gameRunning;

    // Start is called before the first frame update / 
    private void Start() {
        gameRunning = true;
        ingameMusic.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
        StartCoroutine(GameListener());
    }

    // Update is called once per frame / This update captures player key usage for triggering pause
    private void Update()
    {
        if (Input.GetKeyDown("escape") && gameRunning == true) {
            pauseMenu.GetComponent<PauseMenu>().PauseGame();
        }
    }

    public void SetGameStatus(bool status) {
        gameRunning = status;
    }

    IEnumerator GameListener() {
        while (gameRunning == true) {
            print("second test");

            if(timer.GetComponent<Timer>().GetCurrentTime() == "0000") {
                print("game over");
                EndGame();
            }

            if(player.GetComponent<PlayerHealth>().getHealth() == 0) {
                print("health gone baby");
                EndGame();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void EndGame() {


    }
}
