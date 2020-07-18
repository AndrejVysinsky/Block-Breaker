using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        var gameController = GameController.Instance;
        scoreText.text = gameController.GetScore().ToString();

        if (gameController.IsOutOfBalls())
        {
            gameOverText.text = "Game Over";
        }
        else
        {
            gameOverText.text = "You win!";
        }
    }

}
