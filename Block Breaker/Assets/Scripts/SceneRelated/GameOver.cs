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
        scoreText.text = gameController.Score.ToString();

        if (gameController.Balls == 0)
        {
            gameOverText.text = "Game Over";
        }
        else
        {
            gameOverText.text = "You win!";
        }
    }
}
