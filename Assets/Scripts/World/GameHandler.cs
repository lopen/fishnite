using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject ingameMusic;
    [SerializeField] private Timer timer;
    [SerializeField] private Player player;
    [SerializeField] private GameObject GUI;
    [SerializeField] private Score score;

    [SerializeField] private GameObject creditsLateCall;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject returnMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI endScore;


    [SerializeField] private GameObject healthGone;
    [SerializeField] private GameObject timeGone;

    public bool gameRunning;
    private int endStatus;

    // Start is called before the first frame update / 
    private void Start() {
        gameRunning = true;
        player = Player.instance;
        timer = Timer.instance;
        score = Score.instance;
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

    private void OnEnable() {
        MinigameFunc.MinigameLaunch += HideGUI;
        MinigameFunc.MinigameFinish += ShowGUI;
    }

    private void OnDisable() {
        MinigameFunc.MinigameLaunch -= HideGUI;
        MinigameFunc.MinigameFinish -= ShowGUI;
    }

    public void SetGameStatus(bool status) {
        gameRunning = status;
    }

    IEnumerator GameListener() {
        while (gameRunning == true) {
            if(timer.GetCurrentTime() == "0000") {
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
                GUI.SetActive(false);
                ingameMusic.SetActive(false);
                Time.timeScale = 0;
                StopCoroutine(GameListener());
            } else if (gameRunning == false && endStatus == 1) {
                creditsLateCall.SetActive(true);
                healthGone.SetActive(true);
                gameOver.SetActive(true);
                GUI.SetActive(false);
                ingameMusic.SetActive(false);
                endScore.text = score.GetScore().ToString("0000");
                Time.timeScale = 0;
                StopCoroutine(GameListener());
            }
        }
    }

    public void EndGame() {
        PlayerPrefs.SetFloat("HighScore", score.GetScore());
        gameRunning = false;
        creditsPanel.SetActive(true);
        creditsPanel.GetComponent<Image>().canvasRenderer.SetAlpha(0f);
        creditsPanel.GetComponent<Image>().CrossFadeAlpha(255.0f, 200.0f, false);
    }

    private void HideGUI() {
        GUI.GetComponent<CanvasGroup>().alpha = 0;
    }

    private void ShowGUI() {
        GUI.GetComponent<CanvasGroup>().alpha = 1;
    }
}
