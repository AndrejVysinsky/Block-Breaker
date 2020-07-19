using System.Collections;
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
    private bool isSpeedUpButtonActive = false;

    //cached for later ball initialization
    private float speedUpButtonModifier = 0;

    void Start()
    {
        gameController = GameController.Instance;
        paddle = Instantiate(paddle);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LaunchBall();
        }
    }

    private void LaunchBall()
    {
        if (gameController.IsOutOfBalls() == false)
        {
            float distanceY = ball.transform.position.y - paddle.transform.position.y;
            Vector2 ballPosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y + distanceY);

            InstantiateBall(ballPosition);
            gameController.BallLaunched();
        }
    }

    public void InstantiateBall(Vector3 position)
    {
        Ball newBall = Instantiate(ball, position, Quaternion.identity);

        if (isSpeedUpButtonActive)
            newBall.IncreaseSpeedModifier(speedUpButtonModifier);

        balls.Add(newBall);

        gameController.NewBallOnScreen();
    }

    public void SpeedUpButtonToggled(float speedAmount)
    {
        isSpeedUpButtonActive = !isSpeedUpButtonActive;
        speedUpButtonModifier = speedAmount;

        if (isSpeedUpButtonActive)
        {
            IncreaseSpeedModifier(speedAmount);
        }
        else
        {
            DecreaseSpeedModifier(speedAmount);
        }
    }

    public void IncreaseSpeedModifier(float modifier)
    {
        foreach (var ball in balls)
        {
            ball.IncreaseSpeedModifier(modifier);
        }
    }

    public void DecreaseSpeedModifier(float modifier)
    {
        foreach (var ball in balls)
        {
            ball.DecreaseSpeedModifier(modifier);
        }
    }

    public void RemoveBall(Ball ball)
    {
        balls.Remove(ball);
        Destroy(ball.gameObject);

        gameController.BallOutOfScreen();
    }
}
