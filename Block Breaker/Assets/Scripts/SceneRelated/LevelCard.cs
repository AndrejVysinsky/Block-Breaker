using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject starContainer;
    [SerializeField] GameObject[] collectedStars;

    [SerializeField] Sprite lockedSprite;
    [SerializeField] Sprite notCompletedSprite;

    public void PopulateCardData(int levelID, bool isUnlocked, bool isCompleted, int numberOfCollectedStars)
    {
        levelText.text = levelID.ToString();

        if (!isCompleted)
        {
            starContainer.SetActive(false);
            GetComponent<Image>().sprite = notCompletedSprite;
        }

        if (!isUnlocked)
        {
            GetComponent<Image>().sprite = lockedSprite;
            GetComponent<Button>().enabled = false;
            return;
        }

        for (int i = 0; i < numberOfCollectedStars; i++)
        {
            collectedStars[i].SetActive(true);
        }
    }
}
