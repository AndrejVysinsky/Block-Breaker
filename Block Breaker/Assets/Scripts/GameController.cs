using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI ballsText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI timeText;

    private int score = 0;
    private int balls = 5; //amount in reserve
    private int activeBalls = 0; //balls moving on screen

    private void Start()
    {
        scoreText.text = score.ToString();
        ballsText.text = balls.ToString();
    }

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

    public TextMeshProUGUI GetLevelText()
    {
        return levelText;
    }

    public TextMeshProUGUI GetTimeText()
    {
        return timeText;
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

    public bool IsOutOfBalls()
    {
        return balls == 0;
    }

    public void BallLaunched()
    {
        balls--;
        ballsText.text = balls.ToString();
    }

    public void NewBallOnScreen()
    {
        activeBalls++;
    }

    public void BallOutOfScreen()
    {
        activeBalls--;
        SubtractScore(50);

        if (balls == 0 && activeBalls == 0)
        {
            LoadGameOverScene();
        }
    }

    private void LoadGameOverScene()
    {
        sceneLoader.LoadGameOverScene();
    }

    public void LoadNextLevel()
    {
        balls += activeBalls;
        activeBalls = 0;

        scoreText.text = score.ToString();
        ballsText.text = balls.ToString();

        sceneLoader.LoadNextScene();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
