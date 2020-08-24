using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject starContainer;
    [SerializeField] GameObject[] stars;

    [SerializeField] Sprite inactiveStar;
    [SerializeField] Sprite activeStar;

    [Serializable]
    struct LevelCardVariation
    {
        public Sprite defaultSprite;
        public Sprite notCompletedSprite;
        public Sprite lockedSprite;
    }

    [SerializeField] LevelCardVariation normalLevel;
    [SerializeField] LevelCardVariation specialLevel;

    private int specialLevelThreshold = 21;

    public void PopulateCardData(int levelID, bool isUnlocked, bool isCompleted, int numberOfCollectedStars)
    {
        levelText.text = levelID.ToString();

        if (levelID < specialLevelThreshold)
        {
            SetSpriteImage(normalLevel, isUnlocked, isCompleted);
        }
        else
        {
            SetSpriteImage(specialLevel, isUnlocked, isCompleted);
        }

        SetStars(isCompleted, numberOfCollectedStars);
    }

    private void SetSpriteImage(LevelCardVariation levelCardVariation, bool isUnlocked, bool isCompleted)
    {
        var imageComponent = GetComponent<Image>();

        imageComponent.sprite = levelCardVariation.defaultSprite;

        if (!isCompleted)
        {
            imageComponent.sprite = levelCardVariation.notCompletedSprite;
            starContainer.SetActive(false);
        }

        if (!isUnlocked)
        {
            imageComponent.sprite = levelCardVariation.lockedSprite;
            GetComponent<Button>().enabled = false;
        }
    }
    
    private void SetStars(bool isCompleted, int numberOfCollectedStars)
    {
        if (isCompleted)
        {
            for (int i = 0; i < numberOfCollectedStars; i++)
            {
                stars[i].GetComponent<Image>().sprite = activeStar;
            }
        }
        else
        {
            starContainer.SetActive(false);
        }
    }
}
