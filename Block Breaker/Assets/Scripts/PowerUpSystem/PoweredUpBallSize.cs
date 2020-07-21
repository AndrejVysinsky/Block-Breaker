using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class PoweredUpBallSize : PoweredUpBall
{
    private float totalSize;
    private float remainingSize;
    private float sizeChangePerStep;

    public PoweredUpBallSize(Ball ball, float duration, int numberOfSteps, float wearOffTime, float speed)
                            : base(ball, duration, numberOfSteps, wearOffTime)
    {
        totalSize = speed;
        remainingSize = 0;
        sizeChangePerStep = totalSize / numberOfSteps;

        RefreshPowerUp();
    }

    public override void RefreshPowerUp()
    {
        base.RefreshPowerUp();

        poweredUpBall.IncreaseSizeModifier(totalSize - remainingSize);
        remainingSize = totalSize;
    }

    protected override void UpdatePowerUp(int numberOfSteps)
    {
        base.UpdatePowerUp(numberOfSteps);

        UpdateSpeed(numberOfSteps);
    }

    private void UpdateSpeed(int numberOfSteps)
    {
        float speedChange = numberOfSteps * sizeChangePerStep;

        remainingSize -= speedChange;
        poweredUpBall.DecreaseSizeModifier(speedChange);
    }
}
