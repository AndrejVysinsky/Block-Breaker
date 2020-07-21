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
    private int baseBlockScore = 10;

    void Start()
    {
        gameController = GameController.Instance;

        Block[] breakableBlocks = FindObjectsOfType<Block>().Where(b => b.CompareTag("Breakable")).ToArray();
        hitsNeededToDestroyAllBlocks = breakableBlocks.Sum(b => b.GetMaxHits());
        blockCount = breakableBlocks.Count();
    }

    public void RemoveBlock(int maxHits)
    {
        blockCount--;
        gameController.Score += maxHits * baseBlockScore;

        if (blockCount == 0)
        {
            gameController.AddTimeBonus(hitsNeededToDestroyAllBlocks, baseBlockScore);
            gameController.LoadNextLevel();         
        }
    }
}
