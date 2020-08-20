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

    public bool IsPaused { get; set; }

    public int Score { get; set; }
    public int BestScore { get; private set; }
    public int BestStars { get; private set; }

    private IEnumerator inputDelay;

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

        Debug.Log("Pocet blokov: " + blockCount);
    }

    private void LoadLevelData()
    {
        var levelData = ApplicationDataManager.Instance.GetLevelData(SceneManager.GetActiveScene().name);

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
        if (Score > BestScore)
        {
            ApplicationDataManager.Instance.AddLevelData(SceneManager.GetActiveScene().name, Score, GetNumberOfStars());
        }
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
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void StartInputDelay()
    {
        inputDelay = InputDelay();
        StartCoroutine(inputDelay);
    }

    private IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(0.1f);
        inputDelay = null;
    }

    public bool IsInputDelayed()
    {
        return inputDelay != null;
    }
}
