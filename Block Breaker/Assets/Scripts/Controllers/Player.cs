using System.Collections;
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
            if (Input.GetMouseButtonUp(0))
            {
                LaunchDefaultBall();
            }

            if (Input.GetMouseButtonDown(0))
            {
                //ShowBallTrajectory();
            }
        }
    }

    private void LaunchDefaultBall()
    {
        defaultBallLaunched = true;
        Destroy(defaultBall.gameObject);

        paddle.GetComponent<Paddle>().enabled = true;
        
        LaunchBallFromPaddle();
    }

    private void ShowBallTrajectory()
    {
        float mousePosX = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
        float mousePosY = 10;

        Vector3 toPosition = new Vector3(mousePosX, mousePosY, 0);

        Debug.Log($"mouseX: {mousePosX}, ballX: {defaultBall.transform.position.x}");

        trajectoryDisplay.ShowTrajectory(defaultBall.transform.position, toPosition, mousePosY);
    }

    public void LaunchExtraBall()
    {
        if (gameController.Balls > 0)
        {
            LaunchBallFromPaddle();
            gameController.Balls--;
        }
    }

    private void LaunchBallFromPaddle()
    {
        Vector3 ballPosition = ballPrefab.transform.position;
        ballPosition.x = paddle.transform.position.x;
        InstantiateBall(ballPosition);
    }

    public void InstantiateBall(Vector3 position)
    {
        Ball newBall = Instantiate(ballPrefab, position, Quaternion.identity);

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
