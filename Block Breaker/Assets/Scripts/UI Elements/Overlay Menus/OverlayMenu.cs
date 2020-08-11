using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OverlayMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject winScreen;

    [SerializeField] GameObject[] winScreenVariants;

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseScreen.GetComponent<PauseMenu>().DeactivateButtons();
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading menu...");
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameWon()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
