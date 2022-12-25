using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    InGame,
    SkillSelection,
    Dead,
    Pause
}
public class GameManager : Singleton<GameManager>
{
    public CameraShake CameraShake;
    public static Action<GameStates> OnGameStatesChanged;
    public static bool IsGamePaused = false;
    public static bool IsDead = false;
    private GameStates _gameStates;
    [SerializeField] private GameObject _gamePanel;// придумать куда деть
    private void Awake()
    {
        CameraShake = GetComponent<CameraShake>();

    }
    private void Start()
    {
        UpdateGameStates(GameStates.InGame);
    }
    private void Update()
    {
        if (IsDead == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Z))
            {
                if (IsGamePaused)
                {
                    ResumeGame();

                }
                else
                {
                    PauseGame();
                }
            }
        }

    }
    public void UpdateGameStates(GameStates state)
    {
        _gameStates = state;
        switch (state)
        {
            case GameStates.InGame:
                IsDead = false;
                SetActiveGamePanel();
                ChangeTimeScaleToOne();
                break;
            case GameStates.SkillSelection:
                ChangeTimeScaleToZero();
                //SoundManager.Instance.ToggleEffects();
                break;
            case GameStates.Dead:
                IsDead = true;
                ChangeTimeScaleToZero();
                SetEnableGamingPanel();
                break;
            case GameStates.Pause:
                //SoundManager.Instance.ToggleEffects();
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
    public void SetEnableGamingPanel()
    {
        _gamePanel.SetActive(false);
    }

    public void ChangeTimeScaleToZero()
    {
        Time.timeScale = 0.0f;
    }
    public void ChangeTimeScaleToOne()
    {
        Time.timeScale = 1.0f;
    }
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        IsGamePaused = true;
        _gamePanel?.SetActive(false);
        UpdateGameStates(GameStates.Pause);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        IsGamePaused = false;
        UpdateGameStates(GameStates.InGame);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.UpdateGameStates(GameStates.InGame);
    }
    private void OnApplicationFocus(bool focus)
    {
        PauseGame();
    }






}
