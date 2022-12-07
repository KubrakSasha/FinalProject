using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeadMenu : MonoBehaviour
{
    [SerializeField] private GameObject _playerDeadMenu;
    void Start()
    {
        GameManager.OnGameStatesChanged += OnPlayerDeadMenuActivate;
    }

    private void OnPlayerDeadMenuActivate(GameStates state)
    {
        _playerDeadMenu.SetActive(state == GameStates.Dead);
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }


}
