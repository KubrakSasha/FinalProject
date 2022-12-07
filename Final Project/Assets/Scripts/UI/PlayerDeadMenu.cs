using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeadMenu : MonoBehaviour
{
    [SerializeField] private GameObject _playerDeadMenu;
    void Start()
    {
        GameManager.OnGameStatesChanged += OnPlayerDeadMenuActivate;
    }
    public void Reset()
    {
        GameManager.OnGameStatesChanged -= OnPlayerDeadMenuActivate;
        SceneManager.LoadScene(0);
    }
    private void OnPlayerDeadMenuActivate(GameStates state)
    {
        _playerDeadMenu.SetActive(state == GameStates.Dead);
    }

}
