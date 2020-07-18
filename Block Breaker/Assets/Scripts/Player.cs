using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] Ball ball;
    [SerializeField] Paddle paddle;

    private GameController gameController;

    private List<Ball> balls = new List<Ball>();
    private bool isRegularSpeed = true;

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
        Ball b = Instantiate(ball, position, Quaternion.identity);

        if (!isRegularSpeed)
            b.ToggleSpeed();

        balls.Add(b);

        gameController.NewBallOnScreen();
    }

    public void ToggleBallsSpeed()
    {
        foreach (var ball in balls)
            ball.ToggleSpeed();

        isRegularSpeed = !isRegularSpeed;
    }

    public void RemoveBall(Ball ball)
    {
        balls.Remove(ball);
        Destroy(ball.gameObject);

        gameController.BallOutOfScreen();
    }
}
