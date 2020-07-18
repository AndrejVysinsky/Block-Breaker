using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;

    private GameController gameController;

    private TextMeshProUGUI timeText;
    private float startTime;

    private int blockCount = 0;
    //amount of hits needed to destroy all blocks in current level
    private int hitsNeeded = 0;
    private int scoreMultiplier = 10;

    void Start()
    {
        gameController = GameController.Instance;
        gameController.GetLevelText().text = $"Level {SceneManager.GetActiveScene().buildIndex}";
        
        timeText = gameController.GetTimeText();
        startTime = Time.time;

        Block[] breakableBlocks = FindObjectsOfType<Block>().Where(b => b.CompareTag("Breakable")).ToArray();
        hitsNeeded = breakableBlocks.Sum(b => b.GetMaxHits());
        blockCount = breakableBlocks.Count();
    }

    void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        var levelTime = (Time.time - startTime);

        string minutes = Mathf.Floor(levelTime / 60).ToString("00");
        string seconds = (levelTime % 60).ToString("00");

        timeText.text = string.Format("{0}:{1}", minutes, seconds);
    }

    public void RemoveBlock(int maxHits)
    {
        blockCount--;
        gameController.AddScore(maxHits * scoreMultiplier);

        if (blockCount == 0)
        {
            //add time bonus
            var levelTime = (Time.time - startTime);
            int timeBonus = (int)(hitsNeeded * scoreMultiplier - (levelTime % 60));

            if (timeBonus > 0)
                gameController.AddScore(timeBonus);

            gameController.NextLevel();
            sceneLoader.LoadNextScene();
        }
    }
}
