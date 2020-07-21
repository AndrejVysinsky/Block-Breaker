using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

public class PoweredUpBallSpeed : PoweredUpBall
{
    private float totalSpeed;
    private float remainingSpeed;
    private float speedChangePerStep;

    public PoweredUpBallSpeed(Ball ball, float duration, int numberOfSteps, float speed) : base(ball, duration, numberOfSteps)
    {
        totalSpeed = speed;
        remainingSpeed = 0;
        speedChangePerStep = totalSpeed / numberOfSteps;

        RefreshPowerUp();
    }

    public override void RefreshPowerUp()
    {
        base.RefreshPowerUp();

        poweredUpBall.IncreaseSpeedModifier(totalSpeed - remainingSpeed);
        remainingSpeed = totalSpeed;

        //+ reset color
    }

    protected override void UpdatePowerUp(int numberOfSteps)
    {
        base.UpdatePowerUp(numberOfSteps);

        UpdateSpeed(numberOfSteps);
        UpdateColor(numberOfSteps);
    }

    private void UpdateSpeed(int numberOfSteps)
    {
        float speedChange = numberOfSteps * speedChangePerStep;

        remainingSpeed -= speedChange;
        poweredUpBall.DecreaseSpeedModifier(speedChange);
    }

    private void UpdateColor(int numberOfSteps)
    {

    }
}
