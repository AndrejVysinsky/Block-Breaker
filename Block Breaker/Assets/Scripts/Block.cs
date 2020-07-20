using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] int maxHits;
    [SerializeField] ParticleSystem damageParticles;

    //cached references
    private Level level;
    private SpriteRenderer spriteRenderer;
    private PowerUp powerUp;

    //state variables
    private int hitCount = 0;
    private Gradient gradient;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        powerUp = GetComponent<PowerUp>();

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
        ParticleSystem particles = Instantiate(damageParticles, transform.position, transform.rotation);
        
        //get copy of colorOverLifeTime and set gradient
        ColorOverLifetimeModule col = particles.colorOverLifetime;
        col.color = new MinMaxGradient(gradient);

        particles.Play();
        Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constant);

        if (CompareTag("Breakable"))
        {
            hitCount++;
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

    public int GetMaxHits()
    {
        return maxHits;
    }

    private void HandleBlockDamage()
    {
        Color color = spriteRenderer.color;
        color.a = 1 - ((float)hitCount / maxHits);
        spriteRenderer.color = color;
    }

    private void HandleBlockDestroy(GameObject collidingGameObject)
    {
        /*
         * if block has powerup -> powerup will handle destroying in case powerup has some expiration time
         */
        if (powerUp != null)
        {
            powerUp.Collect(collidingGameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        level.RemoveBlock(maxHits);
    }
}
