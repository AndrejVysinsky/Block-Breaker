using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;

    private float displayedScore;
    private int totalScore;

    private void Start()
    {
        displayedScore = 0;
        totalScore = Level.Instance.Score;

        if (bestScoreText != null)
        {
            bestScoreText.text = Level.Instance.BestScore.ToString();
        }
    }

    private void Update()
    {
        Debug.Log(displayedScore);

        if (totalScore > 0)
        {
            CountToPositiveScore();
        }
        else
        {
            CountToNegativeScore();
        }
    }

    private void CountToPositiveScore()
    {
        if (displayedScore < totalScore)
        {
            displayedScore += totalScore * 0.0015f;

            if (displayedScore > totalScore)
            {
                displayedScore = totalScore;
            }

            scoreText.text = ((int)displayedScore).ToString();
        }
    }

    private void CountToNegativeScore()
    {
        if (displayedScore > totalScore)
        {
            displayedScore += totalScore * 0.0015f;

            if (displayedScore < totalScore)
            {
                displayedScore = totalScore;
            }

            scoreText.text = ((int)displayedScore).ToString();
        }
    }
}