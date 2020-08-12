using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] CollectibleSpawner collectibleSpawner;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] OverlayMenu overlayMenu;

    public static Level Instance { get; private set; }

    private int blockCount = 0;

    public int Score { get; set; }
    public int BestScore { get; private set; }
    public int BestStars { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadLevelData();
        CountBreakableBlocks();
    }

    private void LoadLevelData()
    {
        var levelData = PlayerData.Instance.GetLevelData(SceneManager.GetActiveScene().name);

        if (levelData == null)
        {
            BestScore = int.MinValue;
            BestStars = 0;
        }
        else
        {
            BestScore = levelData.Score;
            BestStars = levelData.Stars;
        }
    }

    private void SaveLevelData()
    {
        PlayerData.Instance.AddLevelData(SceneManager.GetActiveScene().name, Score, GetNumberOfStars());
    }

    private void CountBreakableBlocks()
    {
        Block[] breakableBlocks = FindObjectsOfType<Block>().Where(b => b.CompareTag("Breakable")).ToArray();

        blockCount = breakableBlocks.Count();

        int totalBlockScore = breakableBlocks.Sum(x => x.GetBlockScore() * x.GetMaxHits());
        progressBar.SetMaxValue(totalBlockScore);
    }

    public void RemoveBlock(Block block)
    {
        blockCount--;

        collectibleSpawner.TryToSpawnCollectible(block.GetMaxHits(), block.transform.position);

        if (blockCount == 0)
        {
            SaveLevelData();
            overlayMenu.GameWon();
        }
    }

    public void OutOfBalls()
    {
        overlayMenu.GameOver();
    }

    public int GetNumberOfStars()
    {
        return progressBar.GetNumberOfStars();
    }

    public bool IsPointerOverGameObject()
    {
        // Check mouse
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }

        // Check touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
