using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class ScenesManager : Singleton<ScenesManager>
{
    public enum SceneType
    {
        MainScene,
        Levels
    }
    public void LoadToScene(SceneType scene)
    {
        
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
        switch (scene)
        {
            case SceneType.MainScene:
                AudioManager.Instance.PlayMusic(AudioManager.MusicType.Menu);
                break;
            case SceneType.Levels:
                AudioManager.Instance.PlayMusic(AudioManager.MusicType.Gameplay);
                break;
            default:
                Debug.LogWarning("No music assigned for this scene: " + scene);
                break; 
        }
        Time.timeScale = 1;
    }
    //hàm riêng vì unity k cho găn onclick có tham số enum
    public void LoadMainScene()
    {
        LoadToScene(SceneType.MainScene);
    }

    public void LoadLevelScene()
    {
        LoadToScene(SceneType.Levels);
    }


    public void QuitGame()
    {
        Debug.Log("Thoát game");
        Application.Quit();
    }
}