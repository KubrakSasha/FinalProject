using System.Collections;
using UnityEngine;

public class PlayerWinMenu : MonoBehaviour
{
    [SerializeField] private GameObject _playerWinMenu;
    void Start()
    {
        GameManager.OnGameStatesChanged += OnPlayerWinMenuActivate;
    }
    public void Restart()
    {
        GameManager.Instance.RestartGame();
    }
    private void OnPlayerWinMenuActivate(GameStates state)
    {
        

        StartCoroutine(PlayerWinGame());
        _playerWinMenu.SetActive(state == GameStates.Win);
    }
    private void OnDestroy()
    {
        GameManager.OnGameStatesChanged -= OnPlayerWinMenuActivate;
    }
    private IEnumerator PlayerWinGame() 
    {
        yield return new WaitForSeconds(5.0f);
    }
}
