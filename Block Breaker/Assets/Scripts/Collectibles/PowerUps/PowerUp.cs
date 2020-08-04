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

    public virtual void Collect(GameObject gameObject)
    {
        if (gameObject.tag != "Ball")
        {
            return;
        }

        ActivatePowerUp(gameObject);
    }

    protected virtual void ActivatePowerUp(GameObject gameObject)
    {
    }

    public void SpawnCollectible(Vector2 position)
    {
        //select variation
        var random = UnityEngine.Random.Range(0, powerUpVariations.Count());
        var powerUpVariation = powerUpVariations[random];

        //spawn collectible
        var collectiblePosition = new Vector3(position.x, position.y, collectiblePrefab.transform.position.z);
        var spawnedCollectible = Instantiate(collectiblePrefab, collectiblePosition, Quaternion.identity);
        spawnedCollectibles.Add(spawnedCollectible);

        //assign params from powerup variation
        spawnedCollectible.SetValue(powerUpVariation.modifier);
        spawnedCollectible.SetSprite(powerUpVariation.sprite);
    }

    public virtual void OnCollected(Collectible collectible)
    {
        if (spawnedCollectibles.Contains(collectible))
        {
            ActivatePowerUp(collectible.GetCollectibleValue());
        }
    }

    protected virtual void ActivatePowerUp(float modifier)
    {
    }
}
