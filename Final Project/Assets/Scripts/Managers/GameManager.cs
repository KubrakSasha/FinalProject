using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameStates 
{
    MainMenu,
    InGame,
    Dead,
    Pause
}
public class GameManager : Singleton<GameManager>
{
    public static Action<GameStates> OnGameStatesChanged;
    public static bool IsGamePaused = false;
    private GameStates gameStates;
    private void Start()
    {
        UpdateGameStates(GameStates.MainMenu);
    }
    public void UpdateGameStates(GameStates state)
    {
        gameStates = state;
        switch (state)
        {
            case GameStates.MainMenu:
                ChangeTimeScaleToZero();
                break;
            case GameStates.InGame:
                break;
            case GameStates.Dead:
                break;
            case GameStates.Pause:
                break;
            default:
                break;
        }
        OnGameStatesChanged?.Invoke(state);
        Debug.Log(gameStates.ToString());
    }
    public void ChangeTimeScaleToZero() 
    {
        Time.timeScale = 0.0f;
    }
    public void PauseGame()
    {        
        Time.timeScale = 0.0f;        
        IsGamePaused = true;
        GameManager.Instance.UpdateGameStates(GameStates.Pause);
    }
    public void ResumeGame() 
    {
        Time.timeScale = 1.0f;
        IsGamePaused = false;
        GameManager.Instance.UpdateGameStates(GameStates.InGame);
    }





}
