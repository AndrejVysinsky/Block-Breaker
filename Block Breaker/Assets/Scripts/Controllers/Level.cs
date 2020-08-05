using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] CollectibleSpawner collectibleSpawner;
    [SerializeField] ProgressBar progressBar;
    
    private GameController gameController;

    private int blockCount = 0;
    private int hitsNeededToDestroyAllBlocks = 0;
    private int blockScoreMean = 0;

    void Start()
    {
        gameController = GameController.Instance;

        Block[] breakableBlocks = FindObjectsOfType<Block>().Where(b => b.CompareTag("Breakable")).ToArray();

        blockCount = breakableBlocks.Count();
        hitsNeededToDestroyAllBlocks = breakableBlocks.Sum(b => b.GetMaxHits());

        int totalBlockScore = breakableBlocks.Sum(x => x.GetBlockScore() * x.GetMaxHits());

        blockScoreMean = totalBlockScore / blockCount;

        progressBar.SetMaxValue(totalBlockScore);
    }

    public void RemoveBlock(Block block)
    {
        blockCount--;

        gameController.Score += block.GetBlockScore() * block.GetMaxHits();

        collectibleSpawner.TryToSpawnCollectible(block.GetMaxHits(), block.transform.position);

        if (blockCount == 0)
        {
            gameController.AddTimeBonus(hitsNeededToDestroyAllBlocks, blockScoreMean);
            gameController.LoadNextLevel();         
        }
    }
}
