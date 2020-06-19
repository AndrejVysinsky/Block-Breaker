using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        gameController = GameController.Instance;        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameController.BallOutOfScreen();
        Destroy(collision.gameObject);

        if (gameController.IsOutOfBalls())
            SceneManager.LoadScene("Game Over");
    }
}
