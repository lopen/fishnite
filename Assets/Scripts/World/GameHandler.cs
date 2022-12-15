using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu; // Referene to pause menu
    [SerializeField] public GameObject ingameMusic; // Reference to ingame music object
    [SerializeField] private Timer timer; // Reference to ingame timer
    [SerializeField] private Player player; // Reference to player object
    [SerializeField] private GameObject GUI; // Reference to GUI
    [SerializeField] private Score score; // Reference to ingame score counter

    [SerializeField] private GameObject creditsLateCall; // Reference to credit screen, used for late calling buttons to appear after fade
    [SerializeField] private GameObject creditsPanel; // Reference to credits overall panel
    [SerializeField] private GameObject retryButton; // Reference to retry button
    [SerializeField] private GameObject returnMenu; // Reference to return to menu button
    [SerializeField] private GameObject gameOver; // Reference to game over text
    [SerializeField] private TextMeshProUGUI endScore; // Reference to end game score

    [SerializeField] private GameObject healthGone; // Reference to health gone fail condition text
    [SerializeField] private GameObject timeGone; // Reference to time up fail condition text

    public bool gameRunning; // Status bool for game running check
    private int endStatus; // Status bool for end game check

    // Start is called before the first frame update / Assigns instances as well as player pref values, as well as starting our game listener co-routine
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

    // Event listener for minigame
    private void OnEnable() {
        MinigameFunc.MinigameLaunch += HideGUI;
        MinigameFunc.MinigameFinish += ShowGUI;
    }

    // Event listener for minigame
    private void OnDisable() {
        MinigameFunc.MinigameLaunch -= HideGUI;
        MinigameFunc.MinigameFinish -= ShowGUI;
    }

    // Sets game status, used for co-routine
    public void SetGameStatus(bool status) {
        gameRunning = status;
    }

    // Main game listener co-routine, used for calling end game function and late calls to credit screen
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

    // End game function for setting high score and fade in
    public void EndGame() {
        PlayerPrefs.SetFloat("HighScore", score.GetScore());
        gameRunning = false;
        creditsPanel.SetActive(true);
        creditsPanel.GetComponent<Image>().canvasRenderer.SetAlpha(0f);
        creditsPanel.GetComponent<Image>().CrossFadeAlpha(255.0f, 200.0f, false);
    }

    // GUI fade method
    private void HideGUI() {
        GUI.GetComponent<CanvasGroup>().alpha = 0;
    }

    // // GUI show method
    private void ShowGUI() {
        GUI.GetComponent<CanvasGroup>().alpha = 1;
    }
}
