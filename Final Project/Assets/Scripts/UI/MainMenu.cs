using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas canvas;
    private void Awake()
    {
        GameManager.OnGameStatesChanged += OnMainMenuActivate;
    }
    private void OnMainMenuActivate(GameStates state)
    {
        canvas.gameObject.SetActive(state == GameStates.MainMenu);
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
