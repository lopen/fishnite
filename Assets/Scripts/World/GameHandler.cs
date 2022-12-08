using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] private AudioSource ingameMusic;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject player;

    public bool gameRunning;

    // Start is called before the first frame update / 
    private void Start() {
        gameRunning = true;
        player = GameObject.FindGameObjectWithTag("Player");
        timer = GameObject.FindGameObjectWithTag("Timer");
        ingameMusic = GameObject.Find("VendorSpot").GetComponent<AudioSource>(); 
        ingameMusic.volume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
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
