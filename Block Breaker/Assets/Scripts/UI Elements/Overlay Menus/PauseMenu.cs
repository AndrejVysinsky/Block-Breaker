using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject[] earnedStars;

    private void Start()
    {
        //get real value from controller
        int starsEarned = 2;

        for (int i = 0; i < starsEarned; i++)
        {
            earnedStars[i].SetActive(true);
        }
    }
}
