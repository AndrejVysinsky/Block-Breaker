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

    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite lockedSprite;
    [SerializeField] Sprite notCompletedSprite;

    [SerializeField] Sprite defaultSpriteSpecial;
    [SerializeField] Sprite lockedSpriteSpecial;
    [SerializeField] Sprite notCompletedSpriteSpecial;

    [SerializeField] Sprite inactiveStar;
    [SerializeField] Sprite activeStar;

    public void PopulateCardDataV2(int levelID, bool isUnlocked, bool isCompleted, int numberOfCollectedStars)
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
            stars[i].GetComponent<Image>().sprite = activeStar;
        }
    }

    public void PopulateCardData(int levelID, bool isUnlocked, bool isCompleted, int numberOfCollectedStars)
    {
        levelText.text = levelID.ToString();

        SetSpriteImage(levelID, isUnlocked, isCompleted);
        SetStars(isCompleted, numberOfCollectedStars);
    }

    private void SetSpriteImage(int levelID, bool isUnlocked, bool isCompleted)
    {
        var imageComponent = GetComponent<Image>();

        Sprite[] sprites;

        if (levelID <= 20)
        {
            sprites = new Sprite[] { defaultSprite, lockedSprite, notCompletedSprite };
        }
        else
        {
            sprites = new Sprite[] { defaultSpriteSpecial, lockedSpriteSpecial, notCompletedSpriteSpecial };
        }

        imageComponent.sprite = sprites[0];

        if (!isCompleted)
        {
            starContainer.SetActive(false);
            GetComponent<Image>().sprite = sprites[2];
        }

        if (!isUnlocked)
        {
            GetComponent<Image>().sprite = sprites[1];
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
