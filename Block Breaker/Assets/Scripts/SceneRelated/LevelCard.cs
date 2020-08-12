using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject starContainer;
    [SerializeField] GameObject[] collectedStars;

    public void PopulateCardData(int levelID, bool isUnlocked, int numberOfCollectedStars)
    {
        levelText.text = levelID.ToString();

        if (!isUnlocked)
        {
            starContainer.SetActive(false);
            return;
        }

        for (int i = 0; i < numberOfCollectedStars; i++)
        {
            collectedStars[i].SetActive(true);
        }
    }
}
