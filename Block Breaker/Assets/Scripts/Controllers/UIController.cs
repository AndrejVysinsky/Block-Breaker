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
    [SerializeField] TextMeshProUGUI ballsText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] ScoreTextScript scoreTextScript;

    private void Start()
    {
        GameController controller = GameController.Instance;
        GameEventListeners.Instance.AddListener(gameObject);

        controller.ScoreUpdate += OnScoreUpdate;
        controller.BallAmountUpdate += OnBallAmountUpdate;
        controller.LevelUpdate += OnLevelUpdate;

        controller.ForceUpdate();
    }

    private void Update()
    {
        UpdateTime();
    }

    public void OnScoreUpdate(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void OnBallAmountUpdate(int balls)
    {
        ballsText.text = $"x{balls}";
    }

    public void OnLevelUpdate(int level)
    {
        levelText.text = $"Level {level}";
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
    }
}
