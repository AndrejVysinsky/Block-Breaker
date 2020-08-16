using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PowerUp : MonoBehaviour, ICollectedEvent
{
    [Serializable]
    public struct PowerUpVariation
    {
        public float modifier;
        public float spawnChance;
        public Sprite sprite;
    }
    [SerializeField] List<PowerUpVariation> powerUpVariations;

    [SerializeField] Collectible collectiblePrefab;

    private List<Collectible> spawnedCollectibles = new List<Collectible>();

    //cached references
    protected Player player;

    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
        GameEventListeners.Instance.AddListener(gameObject);
    }

    public void SpawnCollectible(Vector2 position)
    {
        //select random variation
        var randomPowerUpVariation = ChooseRandomVariation();

        //spawn collectible
        var collectiblePosition = new Vector3(position.x, position.y, collectiblePrefab.transform.position.z);
        var spawnedCollectible = Instantiate(collectiblePrefab, collectiblePosition, Quaternion.identity);
        spawnedCollectibles.Add(spawnedCollectible);

        //assign params from powerup variation
        spawnedCollectible.SetValue(randomPowerUpVariation.modifier);
        spawnedCollectible.SetSprite(randomPowerUpVariation.sprite);
    }

    private PowerUpVariation ChooseRandomVariation()
    {
        float random = UnityEngine.Random.Range(0, 100);

        foreach (var powerUpVariation in powerUpVariations)
        {
            if (random < powerUpVariation.spawnChance)
            {
                return powerUpVariation;
            }
            random -= powerUpVariation.spawnChance;
        }

        throw new InvalidOperationException(
            "The proportions in the collection do not add up to 1.");
    }

    public virtual void OnCollected(Collectible collectible)
    {
        if (spawnedCollectibles.Contains(collectible))
        {
            ActivatePowerUp(collectible.GetCollectibleValue(), collectible.transform.position);
        }
    }

    protected virtual void ActivatePowerUp(float modifier, Vector3 position)
    {
        ActivatePowerUp(modifier);
    }

    protected virtual void ActivatePowerUp(float modifier)
    {
    }
}
