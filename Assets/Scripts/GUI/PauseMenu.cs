using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuSelf; // Reference to self GameObject
    [SerializeField] private GameObject SettingsMenuSelf; // Reference to settings menu GameObject
    [SerializeField] private GameObject GameHandler; // Reference to game handler

    [SerializeField] private GameObject resumeButton; // Reference to self GameObject
    [SerializeField] private GameObject retryButton; // Self reference to retry button
    [SerializeField] private GameObject settingsButton; // Self reference to settings button
    [SerializeField] private GameObject exitButton; // Self reference to exit button

    [SerializeField] public GameObject musicSource; // Reference to music source
    [SerializeField] private Slider musicSlider; // Reference to settings music slider

    public bool pauseStatus = false; // Status bool for checking current pause status, used to determine user input response
    private bool settingsOpen = false; // Status bool for checking current settings status, used to determine user input response

    // Start is called before the first frame update / Used for grabbing playerpref for music volume and setting listener for change during timescale 0
    void Start()
    {
        musicSlider.value = musicSource.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
        musicSlider.onValueChanged.AddListener(delegate {SetMusicVol();});
    }

    // Update is called once per frame / Listens for user input
    void Update()
    {
        if (Input.GetKeyDown("escape") && pauseStatus == true && settingsOpen == false) {
            ResumeGame();
        } else if (Input.GetKeyDown("escape") && pauseStatus == true && settingsOpen == true) {
            CloseSettings();
        }
    }

    // Called to pause the game
    public void PauseGame() {
        PauseMenuSelf.SetActive(true);
        SettingsMenuSelf.SetActive(false);
        
        resumeButton.SetActive(true);
        retryButton.SetActive(true);
        settingsButton.SetActive(true);
        exitButton.SetActive(true);

        Time.timeScale = 0;

        pauseStatus = true;
        GameHandler.GetComponent<GameHandler>().SetGameStatus(false);
    }

    // Quits the application, functional only during runtime to avoid misuse
    public void QuitGame() {
        SceneManager.LoadScene("MainMenu");
    }

    // Quits the application, functional only during runtime to avoid misuse
    public void ReloadGame() {
        SceneManager.LoadScene("Lake");
        Time.timeScale = 1;
    }

    // Resumes gameplay, sets timeScale
    public void ResumeGame() {
        Time.timeScale = 1;
        PauseMenuSelf.SetActive(false);
        GameHandler.GetComponent<GameHandler>().SetGameStatus(true);

        settingsOpen = false;
        pauseStatus = false;
    }

    // Opens settings menu
    public void OpenSettings() {
        SettingsMenuSelf.SetActive(true);
        
        resumeButton.SetActive(false);
        retryButton.SetActive(false);
        settingsButton.SetActive(false);
        exitButton.SetActive(false);

        settingsOpen = true;
    }

    // Called on close of settings menu
    private void CloseSettings() {
        SettingsMenuSelf.SetActive(false);
        PauseMenuSelf.SetActive(true);
        resumeButton.SetActive(true);
        retryButton.SetActive(true);
        settingsButton.SetActive(true);
        exitButton.SetActive(true);

        settingsOpen = false;
    }

    // Method for setting and storing music volume
    public void SetMusicVol() {
        musicSource.GetComponent<AudioSource>().volume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
}