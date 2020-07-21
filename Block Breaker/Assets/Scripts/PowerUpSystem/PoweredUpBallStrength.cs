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

    public PoweredUpBallStrength(Ball ball, float duration, int numberOfSteps, float wearOffTime, float strength, ParticleSystem particles)
                            : base(ball, duration, numberOfSteps, wearOffTime)
    {
        totalStrength = strength;
        remainingStrength = 0;
        strengthChangePerStep = totalStrength / numberOfSteps;

        strengthParticles = particles;

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

        UpdateSpeed(numberOfSteps);
    }

    private void UpdateSpeed(int numberOfSteps)
    {
        float strengthChange = numberOfSteps * strengthChangePerStep;

        strengthParticles.transform.parent = poweredUpBall.transform;

        remainingStrength -= strengthChange;
        poweredUpBall.DecreaseStrengthModifier((int)strengthChange);
    }

    public ParticleSystem GetParticles()
    {
        return strengthParticles;
    }
}
