using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] GameObject[] earnedStars;

    [SerializeField] GameObject buttonContainer;

    private void Start()
    {
        bestScoreText.text = Level.Instance.BestScore.ToString();

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
