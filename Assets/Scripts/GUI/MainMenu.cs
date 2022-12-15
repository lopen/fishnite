using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private GameObject SettingsMenuSelf; // Self reference to settings menu

   [SerializeField] private GameObject titleObject; // Self reference to title object
   [SerializeField] private GameObject playButton; // Self reference to play button
   [SerializeField] private GameObject settingsButton; // Self reference to settings button
   [SerializeField] private GameObject muteButton; // Self reference to mute button
   [SerializeField] private GameObject exitButton; // Self reference to exit button
   [SerializeField] private TextMeshProUGUI highScore; // Self reference to high score value
   [SerializeField] private GameObject highScoreContainer; // Self reference to high score container

   [SerializeField] private Slider musicSlider; // Self reference to music slider, used in settings and stores same value as ingame setting slider

   private bool settingsOpen; // Check for if settings is open

   // Start is called before the first frame update / Handles assignment of audio sources, player preference pulling and initial formatting/status checks
   void Start() {
      musicSlider.value = this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
      musicSlider.onValueChanged.AddListener(delegate {SetMusicVol();});
      highScore.text = PlayerPrefs.GetFloat("HighScore", 0f).ToString("0000");
   
      settingsOpen = false;
   }

   // Update is called once per frame / Handles input checking as to launch settings menu
   void Update()
   {
      if (Input.GetKeyDown("escape") && settingsOpen == true) {
         CloseMenuSettings();
      }
   }
     
   // Opens settings menu / Sets status of objects, hiding them from view
   public void OpenMenuSettings() {
      SettingsMenuSelf.SetActive(true);
        
      titleObject.SetActive(false);
      playButton.SetActive(false);
      settingsButton.SetActive(false);
      exitButton.SetActive(false);
      muteButton.SetActive(false);
      highScoreContainer.SetActive(false);

      settingsOpen = true;
   }

   // Called on close of settings menu / Sets status of objects, hiding them from view
   public void CloseMenuSettings() {
      SettingsMenuSelf.SetActive(false);

      titleObject.SetActive(true);
      playButton.SetActive(true);
      settingsButton.SetActive(true);
      exitButton.SetActive(true);
      muteButton.SetActive(true);
      highScoreContainer.SetActive(true);

      settingsOpen = false;
   }

   // Provides function to open a specific game scene, handled via UnityEngine.SceneManagement
   public void OpenScene(string sceneName) {
      SceneManager.LoadScene(sceneName);
      Time.timeScale = 1;
   }

   // Quits the application, handled via UnityEngine.SceneManagement
   public void QuitGame() {
      Application.Quit();
   }

   // Method for setting and storing music volume
   private void SetMusicVol() {
      this.GetComponent<AudioSource>().volume = musicSlider.value;
      PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
   }

   // Method for quick muting music volume, re-clicking provides the same functionality as to return sound
   public void MuteMusic() {
      this.GetComponent<AudioSource>().volume = 0f;
      PlayerPrefs.SetFloat("MusicVolume", 0f);
      musicSlider.value = 0f;
   }
}
