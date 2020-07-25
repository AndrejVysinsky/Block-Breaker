using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class PoweredUpBallSpeed : PoweredUpBall
{
    private float totalSpeed;
    private float remainingSpeed;
    private float speedChangePerStep;

    private Gradient gradient;

    public PoweredUpBallSpeed(Ball ball, float duration, int numberOfSteps, float wearOffTime, float speed, Gradient powerUpGradient) 
                            : base(ball, duration, numberOfSteps, wearOffTime)
    {
        totalSpeed = speed;
        remainingSpeed = 0;
        speedChangePerStep = totalSpeed / numberOfSteps;

        gradient = powerUpGradient;

        RefreshPowerUp();
    }

    public override void RefreshPowerUp()
    {
        base.RefreshPowerUp();

        poweredUpBall.IncreaseSpeedModifier(totalSpeed - remainingSpeed);
        remainingSpeed = totalSpeed;

        poweredUpBall.SetColor32(gradient.Evaluate(1));
        poweredUpBall.SetTrailColor32(gradient.Evaluate(1));
    }

    protected override void UpdatePowerUp(int numberOfSteps)
    {
        base.UpdatePowerUp(numberOfSteps);

        UpdateSpeed(numberOfSteps);
        UpdateColor();
    }

    private void UpdateSpeed(int numberOfSteps)
    {
        float speedChange = numberOfSteps * speedChangePerStep;

        remainingSpeed -= speedChange;
        poweredUpBall.DecreaseSpeedModifier(speedChange);
    }

    private void UpdateColor()
    {
        float colorValue = (float)remainingSteps / totalSteps;

        poweredUpBall.SetColor32(gradient.Evaluate(colorValue));
        poweredUpBall.SetTrailColor32(gradient.Evaluate(colorValue));
    }
}
