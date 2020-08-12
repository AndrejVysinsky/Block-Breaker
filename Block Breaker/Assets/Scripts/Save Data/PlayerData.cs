using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    private List<LevelData> savedData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        savedData = new List<LevelData>();
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            savedData = (List<LevelData>)bf.Deserialize(file);
            file.Close();
        }
    }

    private void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, savedData);
        file.Close();
    }

    public void AddLevelData(string levelName, int score, int stars)
    {
        var level = savedData.FirstOrDefault(x => x.LevelName == levelName);

        if (level == null)
        {
            level = new LevelData();

            level.LevelName = levelName;
            level.Score = score;
            level.Stars = stars;

            savedData.Add(level);
        }
        else
        {
            level.Score = score;
            level.Stars = stars;
        }

        SaveData();
    }

    public LevelData GetLevelData(string levelName)
    {
        return savedData.FirstOrDefault(x => x.LevelName == levelName);
    }

    public int GetNumberOfCompletedLevels()
    {
        return savedData.Count;
    }
}
