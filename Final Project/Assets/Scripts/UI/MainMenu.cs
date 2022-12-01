using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu;
    private void Awake()
    {
        GameManager.OnGameStatesChanged += OnMainMenuActivate;
    }
    private void OnMainMenuActivate(GameStates state)
    {
        _mainMenu.SetActive(state == GameStates.MainMenu);
    }
    public void StartGame()
    {
        GameManager.Instance.UpdateGameStates(GameStates.InGame);
        Time.timeScale = 1.0f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
