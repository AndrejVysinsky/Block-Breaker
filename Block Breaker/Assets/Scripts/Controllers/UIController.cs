using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour, IScoreChangedEvent
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] ScoreTextScript scoreTextScript;

    private void Start()
    {
        GameEventListeners.Instance.AddListener(gameObject);

        levelText.text = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        UpdateTime();
    }

    public void UpdateTime()
    {
        int seconds = (int)Time.timeSinceLevelLoad;

        int minutes = seconds / 60;
        seconds %= 60;

        timeText.text = string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));
    }

    public void OnScoreChanged(Vector3 position, int score)
    {
        ScoreTextScript script = Instantiate(scoreTextScript, position, Quaternion.identity);
        
        script.DisplayScore = score.ToString();

        Level.Instance.Score += score;
        DisplayScore(Level.Instance.Score);
    }

    private void DisplayScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
