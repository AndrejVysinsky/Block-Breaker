using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] float baseSpawnChance;
    [Range(0, 10)]
    [SerializeField] float blockHitBonusChance;

    [Serializable]
    public struct Collectible
    {
        public PowerUp powerUp;
        public float spawnChance;
    }

    [SerializeField] Collectible[] collectibles;

    public void TryToSpawnCollectible(int blockHits, Vector2 position)
    {
        int random = UnityEngine.Random.Range(0, 100);

        if (random <= baseSpawnChance + blockHitBonusChance * blockHits)
        {
            SpawnCollectible(position);
        }
    }
    
    private void SpawnCollectible(Vector2 position)
    {
        var collectible = ChooseRandom();

        collectible.powerUp.SpawnCollectible(position);
    }
    
    private Collectible ChooseRandom()
    {
        float random = UnityEngine.Random.Range(0, 100);
        
        foreach(var collectible in collectibles)
        {
            if (random < collectible.spawnChance)
            {
                return collectible;
            }
            random -= collectible.spawnChance;
        }

        throw new InvalidOperationException(
            "The proportions in the collection do not add up to 1.");
    }
}