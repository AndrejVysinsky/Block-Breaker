using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PoweredUpBallStrength : PoweredUpBall
{
    private float totalStrength;
    private float remainingStrength;
    private float strengthChangePerStep;

    private ParticleSystem strengthParticles;

    private ShapeModule particlesShape;
    private float particlesSizeModifier = 1.0f;

    public PoweredUpBallStrength(Ball ball, float duration, int numberOfSteps, float wearOffTime, float strength, ParticleSystem particles)
                            : base(ball, duration, numberOfSteps, wearOffTime)
    {
        totalStrength = strength;
        remainingStrength = 0;
        strengthChangePerStep = totalStrength / numberOfSteps;

        strengthParticles = particles;

        strengthParticles.transform.parent = poweredUpBall.transform;
        particlesShape = strengthParticles.shape;

        RefreshPowerUp();
    }

    public override void RefreshPowerUp()
    {
        base.RefreshPowerUp();

        poweredUpBall.IncreaseStrengthModifier((int)(totalStrength - remainingStrength));
        remainingStrength = totalStrength;
    }

    protected override void UpdatePowerUp(int numberOfSteps)
    {
        base.UpdatePowerUp(numberOfSteps);

        UpdateStrenght(numberOfSteps);
    }

    public void UpdateParticles()
    {
        if (poweredUpBall.GetSizeModifier() != particlesSizeModifier)
        {
            particlesShape.radius /= particlesSizeModifier;
            particlesSizeModifier = poweredUpBall.GetSizeModifier();
            particlesShape.radius *= particlesSizeModifier;
        }

        var colorOverLife = strengthParticles.colorOverLifetime;

        colorOverLife.color = new MinMaxGradient(poweredUpBall.GetTrailGradient());
    }

    private void UpdateStrenght(int numberOfSteps)
    {
        float strengthChange = numberOfSteps * strengthChangePerStep;

        remainingStrength -= strengthChange;
        poweredUpBall.DecreaseStrengthModifier((int)strengthChange);
    }

    public ParticleSystem GetParticles()
    {
        return strengthParticles;
    }
}
