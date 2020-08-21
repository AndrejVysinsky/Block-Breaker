using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] GameObject levelCardContainer;
    [SerializeField] GameObject levelCardPrefab;

    [SerializeField] SceneLoader sceneLoader;

    [SerializeField] AudioSource audioSource;

    private List<string> levels;

    private void Start()
    {
        levels = new List<string>();

        FindAllLevelScenes();

        PopulateScrollView();
    }

    private void FindAllLevelScenes()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string name = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            if (Regex.IsMatch(name, "^Level \\d+$"))
            {
                levels.Add(name);
            }
        }
    }

    private void PopulateScrollView()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        for (int i = 0; i < levels.Count; i++) 
        {
            var levelCard = Instantiate(levelCardPrefab);
            levelCard.transform.SetParent(levelCardContainer.transform, false);

            int index = new int();
            index = buildIndex + i + 1;

            levelCard.GetComponent<Button>().onClick.AddListener(delegate { sceneLoader.LoadSceneAtIndex(index); });
            levelCard.GetComponent<Button>().onClick.AddListener(delegate { audioSource.Play(); });

            PopulateLevelData(levelCard, i);
        }
    }

    private void PopulateLevelData(GameObject levelCard, int i)
    {
        var levelData = ApplicationDataManager.Instance.GetLevelData(levels[i]);

        int stars = 0;
        bool isUnlocked = false;
        bool isCompleted = false;
        if (levelData != null)
        {
            stars = levelData.Stars;
            isUnlocked = true;
            isCompleted = true;
        }
        else
        {
            if (i > 0)
            {
                var previousLevelData = ApplicationDataManager.Instance.GetLevelData(levels[i - 1]);

                if (previousLevelData != null)
                {
                    isUnlocked = true;
                }
            }
            isCompleted = false;
        }

        if (ApplicationDataManager.Instance.GetNumberOfCompletedLevels() == 0 && i == 0)
        {
            isUnlocked = true;
        }

        levelCard.GetComponent<LevelCard>().PopulateCardData(i + 1, isUnlocked, isCompleted, stars);
    }
}
