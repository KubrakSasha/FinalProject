using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas canvas;
    private void Awake()
    {
        GameManager.OnGameStatesChanged += OnPauseMenuActive;      
    }

    private void OnPauseMenuActive(GameStates state)
    {
        canvas.gameObject.SetActive(state == GameStates.Pause);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Z))
        {
            if (GameManager.IsGamePaused)
            {
                ResumeGame();

            }
            else
            {
                GameManager.Instance.PauseGame();
            }
        }
    }
    public void ResumeGame() 
    {
        GameManager.Instance.ResumeGame();
    }
    public void RestartGame() 
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.UpdateGameStates(GameStates.MainMenu);
    }
    public void QuitGame() 
    {
        Application.Quit();
    }
}
