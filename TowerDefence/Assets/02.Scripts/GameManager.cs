using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }
    public void LoadSceneByName(string sceneName)
    {
        Scene targetScene = SceneManager.GetSceneByName(sceneName);
        if(targetScene != null)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log($"Can't find scene of{sceneName}");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
public enum GameState
{
    Idle,
    SelectLevel,
    StartLevel,
    WaitForLevelFinished,
    CompleteLevel,
    FailLevel,
    GameFinished
}