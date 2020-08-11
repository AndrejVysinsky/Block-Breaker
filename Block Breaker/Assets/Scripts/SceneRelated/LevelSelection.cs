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
        int startIndex = SceneManager.GetActiveScene().buildIndex;

        for (int i = startIndex; i < levels.Count + 25; i++) 
        {
            var card = Instantiate(levelCardPrefab);
            card.transform.SetParent(levelCardContainer.transform, false);

            card.GetComponentInChildren<TextMeshProUGUI>().text = (i).ToString();

            int index = new int();
            index = startIndex + 1;

            card.GetComponent<Button>().onClick.AddListener(delegate { sceneLoader.LoadSceneAtIndex(index); });
        }
    }
}
