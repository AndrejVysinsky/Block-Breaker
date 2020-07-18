using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] int maxHits;
    [SerializeField] ParticleSystem damageParticles;

    //cached references
    private Level level;
    private SpriteRenderer mySpriteRenderer;
    private PowerUp myPowerUp;

    //state variables
    private int hitCount = 0;
    private Gradient gradient;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        myPowerUp = GetComponent<PowerUpAddBall>();

        SetupGradient();
    }

    private void SetupGradient()
    {
        Color myColor = mySpriteRenderer.color;

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
                if (myPowerUp != null)
                    myPowerUp.Collect(collision.gameObject);

                Destroy(gameObject);
                level.RemoveBlock(maxHits);
            }
            else
            {
                Color color = mySpriteRenderer.color;
                color.a = 1 - ((float)hitCount / maxHits);
                mySpriteRenderer.color = color;
            }
        }
    }

    public int GetMaxHits()
    {
        return maxHits;
    }
}
