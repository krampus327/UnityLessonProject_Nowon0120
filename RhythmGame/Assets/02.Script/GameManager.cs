using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameState state;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        DontDestroyOnLoad(instance); 
        // DontDestroyOnLoad object ���� 
        // �⺻������ �ٸ� �����ִ� ��ü�鿡 �����ϸ� �ȵȴ�. 
        // �ٸ� ���� �ִ� ��ü���� DontDestroyOnLoad Object �� �������ִ� �����
        // �����ϱ� ���� �����.
    }
    private void Update()
    {
        switch (state)
        {
            case GameState.Select:
                break;
            case GameState.Play:
                break;
            case GameState.WaitForFinish:
                break;
            case GameState.Finish:
                break;
            case GameState.Score:
                break;
            default:
                break;
        }
    }
    public void NextState()
    {
        state++;
    }
    public void TryPlay()
    {
        if (SongSelector.instance.isPlayable)
        {
            MoveSceneTo("Play");
            NextState();
        }
    }
    public void MoveSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
public enum GameState
{
    Select,
    Play,
    WaitForFinish,
    Finish,
    Score,
}
