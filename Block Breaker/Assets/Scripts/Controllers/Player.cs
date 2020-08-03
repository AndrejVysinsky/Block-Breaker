﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] Ball ball;
    [SerializeField] Paddle paddle;

    private GameController gameController;

    private List<Ball> balls = new List<Ball>();

    void Start()
    {
        gameController = GameController.Instance;
        paddle = Instantiate(paddle);
    }

    public void LaunchBall()
    {
        if (gameController.Balls > 0)
        {
            float distanceY = ball.transform.position.y - paddle.transform.position.y;
            Vector2 ballPosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y + distanceY);

            InstantiateBall(ballPosition);
            gameController.Balls--;
        }
    }

    public void InstantiateBall(Vector3 position)
    {
        Ball newBall = Instantiate(ball, position, Quaternion.identity);

        SendBallInitializedMessage(newBall);

        balls.Add(newBall);

        gameController.ActiveBalls++;
    }

    private void SendBallInitializedMessage(Ball ball)
    {
        foreach (GameObject gameObject in GameEventListeners.Instance.listeners)
        {
            ExecuteEvents.Execute<IBallInitializedEvent>(gameObject, null, (x, y) => x.OnBallInitialized(ball));
        }
    }

    public void IncreaseSpeedModifier(float modifier)
    {
        foreach (var ball in balls)
        {
            ball.IncreaseSpeedModifierBy(modifier);
        }
    }

    public void DecreaseSpeedModifier(float modifier)
    {
        foreach (var ball in balls)
        {
            ball.DecreaseSpeedModifierBy(modifier);
        }
    }

    public void RemoveBall(Ball ball)
    {
        ball.SendOutOfScreenMessage();
        int scorePenalty = ball.GetScorePenalty();

        balls.Remove(ball);
        Destroy(ball.gameObject);

        gameController.BallOutOfScreen(scorePenalty);
    }

    public List<Ball> GetBalls()
    {
        return balls;
    }
}
