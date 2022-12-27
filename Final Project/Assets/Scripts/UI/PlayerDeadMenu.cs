using UnityEngine;

public class PlayerDeadMenu : MonoBehaviour
{
    [SerializeField] private GameObject _playerDeadMenu;
    void Start()
    {
        GameManager.OnGameStatesChanged += OnPlayerDeadMenuActivate;
    }
    public void Restart()
    {        
        GameManager.Instance.RestartGame();        
    }
    private void OnPlayerDeadMenuActivate(GameStates state)
    {
        _playerDeadMenu.SetActive(state == GameStates.Dead);
    }
    private void OnDestroy()
    {
        GameManager.OnGameStatesChanged -= OnPlayerDeadMenuActivate;
    }
}