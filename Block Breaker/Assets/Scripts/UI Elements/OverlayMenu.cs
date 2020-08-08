using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OverlayMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject winScreen;

    [Serializable]
    struct WinMessage
    {
        [SerializeField] public GameObject gameObject;
        [SerializeField] public TextMeshProUGUI[] textFields;
    }

    [SerializeField] WinMessage[] winMessages;

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading menu...");
    }

    public void GameLost()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameWon(bool isNewRecord)
    {
        winScreen.SetActive(true);

        if (isNewRecord)
        {
            ShowNewScoreMessage();
        }
        else
        {
            ShowNoNewScoreMessage();
        }

        Time.timeScale = 0f;
    }

    private void ShowNewScoreMessage()
    {
        winMessages[0].gameObject.SetActive(true);
        //new score
        winMessages[0].textFields[0].text = "12345";
    }

    private void ShowNoNewScoreMessage()
    {
        winMessages[1].gameObject.SetActive(true);
        //new score
        winMessages[1].textFields[0].text = "12345";
        //best score
        winMessages[1].textFields[1].text = "20000";
    }
}
