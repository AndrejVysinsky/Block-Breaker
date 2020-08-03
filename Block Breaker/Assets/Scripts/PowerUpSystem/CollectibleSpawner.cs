using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [Serializable]
    public struct Collectible
    {
        public PowerUp powerUp;
        public float spawnChance;
    }

    [SerializeField] Collectible[] collectibles;
    
    public void SpawnCollectible(Vector2 position)
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