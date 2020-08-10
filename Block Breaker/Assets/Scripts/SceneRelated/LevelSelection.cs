using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private List<string> levels;

    private void Start()
    {
        levels = new List<string>();

        FindAllLevelScenes();

        levels.ForEach(x =>
        {
            text.text += x + "\n";
        });
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
}
