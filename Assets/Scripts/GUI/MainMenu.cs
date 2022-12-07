using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     public void OpenScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
        //GameHandler.GameStart();
     }

     public void QuitGame() {
        Application.Quit();
     }
     

}
