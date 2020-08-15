using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ApplicationDataManager : MonoBehaviour
{
    public static ApplicationDataManager Instance { get; private set; }

    [Serializable]
    private class PlayerData
    {
        public List<LevelData> levelData;
        public int numberOfOwnedStars;
        public int numberOfTripleBallPowerups;
        public int numberOfShieldPowerUps;

        public PlayerData()
        {
            levelData = new List<LevelData>();
        }
    }

    private PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeData();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeData()
    {
        playerData = new PlayerData();
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            playerData = (PlayerData)bf.Deserialize(file);
            file.Close();
        }
    }

    private void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, playerData);
        file.Close();
    }

    public void AddLevelData(string levelName, int score, int stars)
    {
        var level = playerData.levelData.FirstOrDefault(x => x.LevelName == levelName);

        if (level == null)
        {
            level = new LevelData();

            level.LevelName = levelName;
            level.Score = score;
            level.Stars = stars;

            playerData.levelData.Add(level);
            playerData.numberOfOwnedStars += stars;
        }
        else
        {
            if (stars > level.Stars)
            {
                playerData.numberOfOwnedStars += stars - level.Stars;
            }

            level.Score = score;
            level.Stars = stars;
        }

        SaveData();
    }

    public LevelData GetLevelData(string levelName)
    {
        return playerData.levelData.FirstOrDefault(x => x.LevelName == levelName);
    }

    public int GetNumberOfCompletedLevels()
    {
        return playerData.levelData.Count;
    }

    public int GetNumberOfOwnedStars()
    {
        return playerData.numberOfOwnedStars;
    }

    public int GetNumberOfTripleBallPowerups()
    {
        return playerData.numberOfTripleBallPowerups;
    }

    public int GetNumberOfShieldPowerUps()
    {
        return playerData.numberOfShieldPowerUps;
    }

    public void EraseData()
    {
        playerData = new PlayerData();
        SaveData();
    }
}
