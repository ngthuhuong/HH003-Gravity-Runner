using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    public void LoadToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("Thoát game");
        Application.Quit();
    }
}