using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PowerUpStrengthParticleVersion : PowerUpWithDuration, IBallInitializedEvent
{
    /*
        NOT USED
        Kept only because of particle system setup
     */

    [SerializeField] ParticleSystem strengthParticles;

    private List<ParticleSystem> activeParciles;

    private Vector3 globalScale;
    private float particlesSizeModifier = 1.0f;

    protected override void Start()
    {
        base.Start();

        modifier = 3.0f;
        duration = 15.0f;
        numberOfSteps = 1;
        wearOffTime = 1.0f;

        activeParciles = new List<ParticleSystem>();

        var prefabScale = strengthParticles.transform.localScale;
        globalScale = new Vector3(prefabScale.x, prefabScale.y, prefabScale.z);
    }

    protected override void Update()
    {
        base.Update();

        if (remainingDuration > 0)
        {
           UpdateParticles();
        }
    }

    protected override void ActivatePowerUp(float newModifier)
    {
        player.GetBalls().ForEach(x =>
        {
            x.DecreaseStrengthModifierBy((int)remainingModifier);
            x.IncreaseStrengthModifierBy((int)newModifier);

            if (IsExpired())
            {
                InstantiateParticles(x);
            }
        });

        base.ActivatePowerUp(newModifier);
    }

    protected override void UpdatePowerUp(float modifierChange)
    {
        base.UpdatePowerUp(modifierChange);

        player.GetBalls().ForEach(x => x.DecreaseStrengthModifierBy((int)modifierChange));

        if (IsExpired())
        {
            DestroyParticles();
        }
    }

    private void InstantiateParticles(Ball ball)
    {
        ParticleSystem particles = Instantiate(strengthParticles, ball.transform.position, ball.transform.rotation);

        ColorOverLifetimeModule colorOverLifetime = particles.colorOverLifetime;
        colorOverLifetime.color = new MinMaxGradient(ball.GetTrailGradient());

        particles.Play();

        particles.transform.parent = ball.transform;
        particles.transform.localScale = globalScale;

        activeParciles.Add(particles);
    }

    private void UpdateParticles()
    {
        var ball = player.GetBalls().FirstOrDefault();

        if (ball != null)
        {
            if (ball.GetSizeModifier() != particlesSizeModifier)
            {
                UpdateParticleSize(ball.GetSizeModifier());
            }
        }
    }

    private void UpdateParticleSize(float sizeModifier)
    {
        globalScale /= particlesSizeModifier;
        particlesSizeModifier = sizeModifier;
        globalScale *= particlesSizeModifier;

        activeParciles.RemoveAll(x => x == null);
        activeParciles.ForEach(x => x.transform.localScale = globalScale);
    }

    public void OnBallInitialized(Ball ball)
    {
        if (IsExpired() == false)
        {
            ball.IncreaseStrengthModifierBy((int)remainingModifier);
            InstantiateParticles(ball);
        }
    }

    private void DestroyParticles()
    {
        activeParciles.ForEach(x => 
        {
            if (x != null)
            {
                Destroy(x.gameObject);
            }
        });
        activeParciles.Clear();
    }
}