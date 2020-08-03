using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PowerUpStrength : PowerUpWithDuration, IBallInitializedEvent
{
    [SerializeField] ParticleSystem strengthParticles;
    [SerializeField] GameObject point;

    private List<Transform> particleTransforms;
    private float particlesSizeModifier = 1.0f;

    protected override void Start()
    {
        base.Start();

        modifier = 3.0f;
        duration = 15.0f;
        numberOfSteps = 1;
        wearOffTime = 1.0f;

        particleTransforms = new List<Transform>();
    }

    protected override void Update()
    {
        base.Update();

        if (remainingDuration > 0)
        {
           UpdateParticles();
        }
    }

    protected override void ActivatePowerUp(float modifier)
    {
        List<Ball> balls = player.GetBalls();

        balls.ForEach(x => x.DecreaseStrengthModifierBy((int)remainingModifier));
        balls.ForEach(x =>
        {
            x.IncreaseStrengthModifierBy((int)modifier);
            var particles = InstantiateParticles(x);

            Destroy(particles.gameObject, duration);
        });

        base.ActivatePowerUp(modifier);
    }

    protected override void UpdatePowerUp(float modifierChange)
    {
        base.UpdatePowerUp(modifierChange);

        player.GetBalls().ForEach(x => x.DecreaseStrengthModifierBy((int)modifierChange));
    }

    private ParticleSystem InstantiateParticles(Ball ball)
    {
        ParticleSystem particles = Instantiate(strengthParticles, ball.transform.position, ball.transform.rotation);

        ColorOverLifetimeModule colorOverLifetime = particles.colorOverLifetime;
        colorOverLifetime.color = new MinMaxGradient(ball.GetTrailGradient());

        particles.Play();

        var scale = particles.transform.localScale;
        particles.transform.parent = ball.transform;
        particles.transform.localScale = scale;

        particleTransforms.Add(particles.transform);

        return particles;
    }

    public void UpdateParticles()
    {
        var ball = player.GetBalls().FirstOrDefault();

        if (ball != null)
        {
                particleTransforms.RemoveAll(x => x == null);

                Debug.Log(particleTransforms.Count());

                particleTransforms.ForEach(x =>
                {
                    x.localScale /= particlesSizeModifier;

                    particlesSizeModifier = ball.GetSizeModifier();

                    x.localScale *= particlesSizeModifier;
                });

            //var colorOverLife = strengthParticles.colorOverLifetime;

            //colorOverLife.color = new MinMaxGradient(ball.GetTrailGradient());
        }
    }

    public void OnBallInitialized(Ball ball)
    {
        if (remainingModifier > 0)
        {
            ball.IncreaseStrengthModifierBy((int)remainingModifier);
            var particles = InstantiateParticles(ball);
            Destroy(particles.gameObject, remainingDuration);
        }
    }
}