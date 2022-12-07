using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject ingameMusic;

    public bool gameRunning;

    private void Start() {
        gameRunning = true;
        ingameMusic.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape") && gameRunning == true) {
            pauseMenu.GetComponent<PauseMenu>().PauseGame();
        }
    }

    public void SetGameStatus(bool status) {
        gameRunning = status;
    }
}
