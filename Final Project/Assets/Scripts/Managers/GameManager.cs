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
    private GameStates _gameStates;
    [SerializeField] private GameObject _gamePanel;// придумать куда деть
     
    private void Start()
    {
        UpdateGameStates(GameStates.MainMenu);
    }
    public void UpdateGameStates(GameStates state)
    {
        _gameStates = state;
        switch (state)
        {
            case GameStates.MainMenu:
                ChangeTimeScaleToZero();
                break;
            case GameStates.InGame:
                SetActiveGamePanel();   
                break;
            case GameStates.Dead:
                ChangeTimeScaleToZero();
                break;
            case GameStates.Pause:
                break;
            default:
                break;
        }
        OnGameStatesChanged?.Invoke(state);
        Debug.Log(_gameStates.ToString());
    }

    private void SetActiveGamePanel()
    {
        _gamePanel.SetActive(true);
    }

    public void ChangeTimeScaleToZero() 
    {
        Time.timeScale = 0.0f;
    }
    public void PauseGame()
    {        
        Time.timeScale = 0.0f;        
        IsGamePaused = true;
        _gamePanel?.SetActive(false);
        GameManager.Instance.UpdateGameStates(GameStates.Pause);
    }
    public void ResumeGame() 
    {
        Time.timeScale = 1.0f;
        IsGamePaused = false;

        GameManager.Instance.UpdateGameStates(GameStates.InGame);
    }





}
