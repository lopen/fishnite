using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private GameObject SettingsMenuSelf;

   [SerializeField] private GameObject titleObject;
   [SerializeField] private GameObject playButton;
   [SerializeField] private GameObject settingsButton;
   [SerializeField] private GameObject muteButton;
   [SerializeField] private GameObject exitButton;
   [SerializeField] private TextMeshProUGUI highScore;
   [SerializeField] private GameObject highScoreContainer;

   [SerializeField] private Slider musicSlider;

   private bool settingsOpen;
   private float previousVol;

   void Start() {
      musicSlider.value = this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
      musicSlider.onValueChanged.AddListener(delegate {SetMusicVol();});
      highScore.text = PlayerPrefs.GetFloat("HighScore", 0f).ToString("00000");
   
      settingsOpen = false;
   }

   // Update is called once per frame
   void Update()
   {
      if (Input.GetKeyDown("escape") && settingsOpen == true) {
         CloseMenuSettings();
      }
   }
     
   // Opens settings menu
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

   // Called on close of settings menu
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

   public void OpenScene(string sceneName) {
      SceneManager.LoadScene(sceneName);
   }

   public void QuitGame() {
      Application.Quit();
   }

   // Method for setting and storing music volume
   private void SetMusicVol() {
      this.GetComponent<AudioSource>().volume = musicSlider.value;
      PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
   }

   public void MuteMusic() {
      this.GetComponent<AudioSource>().volume = 0f;
      PlayerPrefs.SetFloat("MusicVolume", 0f);
      musicSlider.value = 0f;
   }

   public void SetMusicTrack() {
      print("Test");
   }
}
