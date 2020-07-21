using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PoweredUpBallStrength : PoweredUpBall
{
    private float totalStrength;
    private float remainingStrength;
    private float strengthChangePerStep;

    public PoweredUpBallStrength(Ball ball, float duration, int numberOfSteps, float wearOffTime, float strength)
                            : base(ball, duration, numberOfSteps, wearOffTime)
    {
        totalStrength = strength;
        remainingStrength = 0;
        strengthChangePerStep = totalStrength / numberOfSteps;

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

        remainingStrength -= strengthChange;
        poweredUpBall.DecreaseStrengthModifier((int)strengthChange);
    }

}
