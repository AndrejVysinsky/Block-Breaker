using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] GameObject[] earnedStars;

    [SerializeField] GameObject buttonContainer;

    private void Start()
    {
        int bestScore = Level.Instance.BestScore;

        if (bestScore == int.MinValue)
        {
            bestScoreText.text = "You have not completed this level!";
        }
        else
        {
            bestScoreText.text = bestScore.ToString();
        }

        int starsEarned = Level.Instance.BestStars;

        for (int i = 0; i < starsEarned; i++)
        {
            earnedStars[i].SetActive(true);
        }
    }

    public void DeactivateButtons()
    {
        buttonContainer.SetActive(false);
    }
}
