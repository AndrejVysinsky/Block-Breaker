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

        scoreText.text = displayedScore.ToString();

        if (bestScoreText != null)
        {
            bestScoreText.text = Level.Instance.BestScore.ToString();
        }
    }

    private void Update()
    {
        if (Mathf.Abs(displayedScore) < Mathf.Abs(totalScore))
        {
            displayedScore += totalScore * 0.0015f;

            if (Mathf.Abs(displayedScore) > Mathf.Abs(totalScore))
            {
                displayedScore = totalScore;
            }

            scoreText.text = ((int)displayedScore).ToString();
        }
    }
}