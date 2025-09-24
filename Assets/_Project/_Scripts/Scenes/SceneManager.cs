using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class ScenesManager : Singleton<ScenesManager>
{
    public void LoadToScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        switch (sceneName.ToLower())
        {
            case "mainscene":
                AudioManager.Instance.PlayMusic(AudioManager.MusicType.Menu);
                break;
            case "levels":
                AudioManager.Instance.PlayMusic(AudioManager.MusicType.Gameplay);
                break;
            default:
                Debug.LogWarning("No music assigned for this scene: " + sceneName);
                break; 
        }

        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("Thoát game");
        Application.Quit();
    }
}