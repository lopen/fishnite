using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuSelf;
    [SerializeField] private GameObject SettingsMenuSelf;
    [SerializeField] private GameObject GameHandler;

    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject exitButton;

    [SerializeField] public GameObject musicSource;
    [SerializeField] public Slider musicSlider;

    public bool pauseStatus = false;
    private bool settingsOpen = false;
    private float musicVol;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = musicSource.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
        musicSlider.onValueChanged.AddListener(delegate {SetMusicVol();});
    }

    // Update is called once per frame
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
        settingsButton.SetActive(true);
        exitButton.SetActive(true);

        Time.timeScale = 0;

        pauseStatus = true;
        GameHandler.GetComponent<GameHandler>().SetGameStatus(false);
    }

    // Quits the application, functional only during runtime to avoid misuse
    public void QuitGame() {
        Application.Quit();
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
        settingsButton.SetActive(false);
        exitButton.SetActive(false);

        settingsOpen = true;
    }

    // Called on close of settings menu
    private void CloseSettings() {
        SettingsMenuSelf.SetActive(false);
        PauseMenuSelf.SetActive(true);
        resumeButton.SetActive(true);
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