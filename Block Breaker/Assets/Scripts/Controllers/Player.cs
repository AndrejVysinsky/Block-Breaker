﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] Ball ballPrefab;
    [SerializeField] Paddle paddle;
    [SerializeField] TrajectoryDisplay trajectoryDisplay;

    private GameController gameController;

    private List<Ball> balls = new List<Ball>();

    private Ball defaultBall;
    private bool defaultBallLaunched = false;

    private Camera mainCamera;
    private Vector3 mousePosition;

    void Start()
    {
        gameController = GameController.Instance;
        paddle = Instantiate(paddle);

        paddle.GetComponent<Paddle>().enabled = false;

        defaultBall = Instantiate(ballPrefab);
        defaultBall.SetVelocityVector(new Vector2(0, 0));
        defaultBall.transform.parent = paddle.transform;

        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (defaultBallLaunched == false)
        {
            if (Input.GetMouseButton(0))
            {
                CalculateMousePosition();
                trajectoryDisplay.ShowTrajectory(defaultBall.transform.position, mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                LaunchDefaultBall();
                trajectoryDisplay.HideTrajectory();
            }
        }
    }

    private void CalculateMousePosition()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.y < defaultBall.transform.position.y + 3)
            mousePosition.y = defaultBall.transform.position.y + 3;
    }

    private void LaunchDefaultBall()
    {
        defaultBallLaunched = true;
        Destroy(defaultBall.gameObject);

        paddle.GetComponent<Paddle>().enabled = true;

        var ball = LaunchBallFromPaddle();

        float velocity = ball.GetBaseVelocity();

        Vector2 direction = mousePosition - ball.transform.position;

        float velPerUnit = velocity / (Mathf.Abs(direction.x) + Mathf.Abs(direction.y));

        ball.SetVelocityVector(new Vector2(velPerUnit * direction.x, velPerUnit * direction.y));
    }

    public void LaunchExtraBall()
    {
        if (gameController.Balls > 0)
        {
            LaunchBallFromPaddle();
            gameController.Balls--;
        }
    }

    private Ball LaunchBallFromPaddle()
    {
        Vector3 ballPosition = ballPrefab.transform.position;
        ballPosition.x = paddle.transform.position.x;
        return InstantiateBall(ballPosition);
    }

    public Ball InstantiateBall(Vector3 position)
    {
        Ball newBall = Instantiate(ballPrefab, position, Quaternion.identity);

        SendBallInitializedMessage(newBall);

        balls.Add(newBall);

        gameController.ActiveBalls++;

        return newBall;
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
