using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject ingameMusic;
    [SerializeField] public GameObject timer;
    [SerializeField] public GameObject player;

    [SerializeField] private GameObject creditsLateCall;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject returnMenu;
    [SerializeField] private GameObject gameOver;

    [SerializeField] private GameObject healthGone;
    [SerializeField] private GameObject timeGone;

    public bool gameRunning;
    private int endStatus;

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
            if(timer.GetComponent<Timer>().GetCurrentTime() == "0000") {
                endStatus = 0;
                EndGame();
            }

            if(player.GetComponent<PlayerHealth>().getHealth() == 0) {
                endStatus = 1;
                EndGame();
            }

            yield return new WaitForSeconds(1f);
            if (gameRunning == false && endStatus == 0) {
                creditsLateCall.SetActive(true);
                timeGone.SetActive(true);
                gameOver.SetActive(true);
                Time.timeScale = 0;
            } else if (gameRunning == false && endStatus == 1) {
                creditsLateCall.SetActive(true);
                healthGone.SetActive(true);
                gameOver.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void EndGame() {
        gameRunning = false;
        creditsPanel.SetActive(true);
        creditsPanel.GetComponent<Image>().canvasRenderer.SetAlpha(0f);
        creditsPanel.GetComponent<Image>().CrossFadeAlpha(255.0f, 200.0f, false);
    }
}
