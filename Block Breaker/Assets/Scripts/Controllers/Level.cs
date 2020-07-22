using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
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
        blockScoreMean = breakableBlocks.Sum(x => x.GetBlockScore()) / blockCount;
    }

    public void RemoveBlock(int scoreToAdd)
    {
        blockCount--;
        gameController.Score += scoreToAdd;

        if (blockCount == 0)
        {
            gameController.AddTimeBonus(hitsNeededToDestroyAllBlocks, blockScoreMean);
            gameController.LoadNextLevel();         
        }
    }
}
