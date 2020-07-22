using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.ParticleSystem;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] int maxHits;
    [SerializeField] int blockScore = 10;
    [SerializeField] ParticleSystem damageParticles;
    [SerializeField] PowerUp powerUp;

    //cached references
    private Level level;
    private SpriteRenderer spriteRenderer;

    //state variables
    private int hitCount = 0;
    private Gradient gradient;

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
        InstantiateParticles(collision);

        if (CompareTag("Breakable"))
        {
            hitCount += collision.gameObject.GetComponent<Ball>().GetStrength();
            if (hitCount >= maxHits)
            {
                HandleBlockDestroy(collision.gameObject);
            }
            else
            {
                HandleBlockDamage();
            }
        }
    }

    private void InstantiateParticles(Collision2D collision)
    {
        ParticleSystem particles = Instantiate(damageParticles, collision.GetContact(0).point, transform.rotation);

        //get copy of colorOverLifeTime and set gradient
        ColorOverLifetimeModule col = particles.colorOverLifetime;
        col.color = new MinMaxGradient(gradient);

        particles.Play();
        Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constant);
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
        if (powerUp != null)
        {
            powerUp.Collect(collidingGameObject);
        }
        
        level.RemoveBlock(maxHits * blockScore);

        foreach (GameObject gameObject in GameEventListeners.Instance.listeners)
        {
            ExecuteEvents.Execute<IScoreChangeEvent>(gameObject, null, (x, y) => x.DisplayScoreChangeText(transform.position, maxHits * blockScore));
        }

        Destroy(gameObject);
    }
}
