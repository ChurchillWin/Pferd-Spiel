using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField]
    GameObject winPanel;
    public delegate void ResetButtonAction();
    public static event ResetButtonAction OnReset;
    private void OnEnable()
    {
        GameBoard.OnGameWon += GameWon;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Reset()
    {
        OnReset?.Invoke();
        winPanel.SetActive(false);
    }
    public void GameWon()
    {
        winPanel.SetActive(true);
    }
    private void OnDisable()
    {
        GameBoard.OnGameWon -= GameWon;
    }
}
