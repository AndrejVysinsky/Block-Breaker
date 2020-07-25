using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] GameObject sceneLoaderPrefab;

    private SceneLoader sceneLoader;

    public event Action<int> ScoreUpdate;
    public event Action<int> BallAmountUpdate;
    public event Action<int> LevelUpdate;

    private int score = 0;
    public int Score 
    { 
        get 
        { 
            return score; 
        } 
        set 
        {
            ScoreUpdate(value);
            score = value;
        }
    }

    private int balls = 5; //amount in reserve
    public int Balls
    {
        get
        {
            return balls;
        }
        set
        {
            BallAmountUpdate(value);
            balls = value;
        }
    }

    public int ActiveBalls { get; set; } = 0;
    
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

    public void ForceUpdate()
    {
        ScoreUpdate(Score);
        BallAmountUpdate(Balls);
        LevelUpdate(SceneManager.GetActiveScene().buildIndex);

        sceneLoader = Instantiate(sceneLoaderPrefab).GetComponent<SceneLoader>();
    }

    public void BallOutOfScreen(int scorePenalty)
    {
        ActiveBalls--;
        Score += scorePenalty;

        if (Balls == 0 && ActiveBalls == 0)
        {
            sceneLoader.LoadGameOverScene();
        }
    }

    public void LoadNextLevel()
    {
        Balls += ActiveBalls;
        ActiveBalls = 0;
        
        sceneLoader.LoadNextScene();
    }

    public void AddTimeBonus(int hitsNeeded, int blockScoreMean)
    {
        int levelTime = (int)Time.timeSinceLevelLoad;
        int timeBonus = hitsNeeded * blockScoreMean - (levelTime % 60);

        if (timeBonus > 0)
        {
            Score += timeBonus;
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
