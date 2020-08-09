using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;

    private int displayedScore;
    private int totalScore;

    private void Start()
    {
        displayedScore = 0;
        totalScore = 1234;

        if (bestScoreText != null)
        {
            bestScoreText.text = "11111";
        }
    }

    private void Update()
    {
        if (displayedScore < totalScore)
        {
            displayedScore += (int)(totalScore * 0.0015f);

            if (displayedScore > totalScore)
            {
                displayedScore = totalScore;
            }

            scoreText.text = displayedScore.ToString();
        }
    }
}