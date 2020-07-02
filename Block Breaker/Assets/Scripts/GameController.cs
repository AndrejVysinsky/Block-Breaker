using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI ballsText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI levelText;

    private int score = 0;
    private int balls = 5; //amount in reserve
    private int activeBalls = 0; //balls on moving on screen
    private float startTime = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        var levelTime = (Time.time - startTime);

        string minutes = Mathf.Floor(levelTime / 60).ToString("00");
        string seconds = (levelTime % 60).ToString("00");

        timeText.text = string.Format("{0}:{1}", minutes, seconds);
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }

    public void SubtractScore(int scoreToSubtract)
    {
        score -= scoreToSubtract;
        scoreText.text = score.ToString();
    }
    
    public int GetScore()
    {
        return score;
    }

    public bool TryToShootBall()
    {
        if (balls == 0)
            return false;

        balls--;
        activeBalls++;
        ballsText.text = balls.ToString();

        return true;
    }

    public void BallOutOfScreen()
    {
        activeBalls--;
        SubtractScore(50);
    }

    public bool IsOutOfBalls()
    {
        return balls == 0 && activeBalls == 0;
    }

    public void NextLevel()
    {
        balls += activeBalls;
        activeBalls = 0;

        //add time bonus score
        if (startTime != 0)
            score += (int)Time.time;

        scoreText.text = score.ToString();
        ballsText.text = balls.ToString();
        startTime = Time.time;
        levelText.text = "Level " + SceneManager.GetActiveScene().buildIndex;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
