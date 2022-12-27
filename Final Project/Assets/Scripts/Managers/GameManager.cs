using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    InGame,
    SkillSelection,
    Dead,
    Pause,
    Win
}
public class GameManager : Singleton<GameManager>
{
    public CameraShake CameraShake;
    public static Action<GameStates> OnGameStatesChanged;
    public static bool IsGamePaused = false;
    public static bool IsDeadOrWin = false;
    public static bool IsScillSelection = false;
    private GameStates _gameStates;
    [SerializeField] private GameObject _gamePanel;    // придумать куда деть

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
        if (IsDeadOrWin == false && IsScillSelection == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
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
                IsDeadOrWin = false;
                IsScillSelection = false;
                SetActiveGamePanel();
                ChangeTimeScaleToOne();
                AudioListener.pause = false;
                break;
            case GameStates.SkillSelection:
                ChangeTimeScaleToZero();
                IsScillSelection = true;
                AudioListener.pause = true;
                //SoundManager.Instance.ToggleEffects();
                break;
            case GameStates.Dead:
                IsDeadOrWin = true;
                ChangeTimeScaleToZero();
                SetEnableGamingPanel();
                break;
            case GameStates.Pause:
                //SoundManager.Instance.ToggleEffects();
                break;
            case GameStates.Win:
                IsDeadOrWin = true;
                ChangeTimeScaleToZero();
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
    private void SetEnableGamingPanel()
    {
        _gamePanel.SetActive(false);
    }

    private void ChangeTimeScaleToZero()
    {
        Time.timeScale = 0.0f;
    }
    private void ChangeTimeScaleToOne()
    {
        Time.timeScale = 1.0f;
    }
    private void PauseGame()
    {
        AudioListener.pause = true;
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
