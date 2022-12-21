using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    private void Awake()
    {
        GameManager.OnGameStatesChanged += OnPauseMenuActive;
    }

    private void OnPauseMenuActive(GameStates state)
    {
        _pauseMenu.SetActive(state == GameStates.Pause);
    }
    private void OnDestroy()
    {
        GameManager.OnGameStatesChanged -= OnPauseMenuActive;
    }
    
    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }
    public void RestartGame()
    {
        //GameManager.OnGameStatesChanged -= OnPauseMenuActive;
        GameManager.Instance.RestartGame();
    }
    public void QuitGame()
    {
        Application.Quit();

    }
}
