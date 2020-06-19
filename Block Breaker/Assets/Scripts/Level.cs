using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] uint blockCount = 0;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] Ball ball;
    [SerializeField] Paddle paddle;

    private GameController gameController;

    private List<Ball> balls = new List<Ball>();

    void Start()
    {
        paddle = Instantiate(paddle);
        gameController = GameController.Instance;
        gameController.NextLevel();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        if (gameController.TryToShootBall())
        { 
            float distanceY = ball.transform.position.y - paddle.transform.position.y;
            Vector2 ballPosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y + distanceY);

            Ball b = Instantiate(ball, ballPosition, Quaternion.identity);
            balls.Add(b);
        }
    }

    public void AddBlock()
    {
        blockCount++;
    }

    public void RemoveBlock()
    {
        blockCount--;

        gameController.AddScore(10);

        if (blockCount == 0)
            sceneLoader.LoadNextScene();
    }
}
