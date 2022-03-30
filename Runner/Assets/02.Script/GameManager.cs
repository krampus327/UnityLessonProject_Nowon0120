using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public static GameManager instance;
    public static GameState gameState;

    public static void Play()
    {
        gameState = GameState.StartGame;
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        switch(GameState)
    }
}

public enum GameState
{
    Idle,
    StartGame,
    SpawnMap,
    WaitForMapSpawned,
    StartRun,
    WaitForGameFinished,
    Finish
}
