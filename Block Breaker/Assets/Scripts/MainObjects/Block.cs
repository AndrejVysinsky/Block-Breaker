using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.ParticleSystem;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] int maxHits;
    [SerializeField] int blockScore = 10;
    [SerializeField] ParticleSystem damageParticles;

    //cached references
    private Level level;
    private SpriteRenderer spriteRenderer;

    //state variables
    private int hitCount = 0;
    private Gradient gradient;

    private bool isBeingDestroyed = false;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //for particles
        SetupGradient();
    }

    private void SetupGradient()
    {
        Color myColor = spriteRenderer.color;

        gradient = new Gradient()
        {
            alphaKeys = new GradientAlphaKey[2]
            {
               new GradientAlphaKey(1.0f, 0.0f),
               new GradientAlphaKey(0.0f, 1.0f)
            },
            colorKeys = new GradientColorKey[2]
            {
                new GradientColorKey(myColor, 0.0f),
                new GradientColorKey(myColor, 1.0f)
            },
            mode = GradientMode.Blend
        };
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Breakable"))
        {
            if (GameController.Instance.isPerformanceModeActive == false)
            {
                InstantiateParticles(collision);
            }

            hitCount += collision.gameObject.GetComponent<Ball>().GetStrength();
            if (hitCount >= maxHits)
            {
                if (isBeingDestroyed == false)
                {
                    HandleBlockDestroy(collision.gameObject);
                    isBeingDestroyed = true;
                }
            }
            else
            {
                HandleBlockDamage();
            }
        }
    }

    private void InstantiateParticles(Collision2D collision)
    {
        ParticleSystem particles = ParticleController.Instance.GetDamageParticles();
        particles.transform.position = collision.GetContact(0).point;

        //get copy of colorOverLifeTime and set gradient
        ColorOverLifetimeModule col = particles.colorOverLifetime;
        col.color = new MinMaxGradient(gradient);

        particles.Play();
    }

    public int GetMaxHits()
    {
        return maxHits;
    }

    public int GetBlockScore()
    {
        return blockScore;
    }

    private void HandleBlockDamage()
    {
        Color color = spriteRenderer.color;
        color.a = 1 - ((float)hitCount / maxHits);
        spriteRenderer.color = color;
    }

    private void HandleBlockDestroy(GameObject collidingGameObject)
    {
        level.RemoveBlock(this);

        foreach (GameObject gameObject in GameEventListeners.Instance.listeners)
        {
            ExecuteEvents.Execute<IScoreChangedEvent>(gameObject, null, (x, y) => x.OnScoreChanged(transform.position, maxHits * blockScore));
        }

        Destroy(gameObject);
    }
}
