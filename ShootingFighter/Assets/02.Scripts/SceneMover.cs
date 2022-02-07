using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    static public SceneMover instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    public int currentIndex = 0;
    [SerializeField] private GameObject gameOver;
    public void MoveSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void MoveSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void MoveNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(currentSceneIndex + 1);
        else
        {
            Instantiate(gameOver);
        }
    }
}
