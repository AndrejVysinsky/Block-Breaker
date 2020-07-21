using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PoweredUpBallSize : PoweredUpBall
{
    private float totalSize;
    private float remainingSize;
    private float sizeChangePerStep;

    public PoweredUpBallSize(Ball ball, float duration, int numberOfSteps, float wearOffTime, float size)
                            : base(ball, duration, numberOfSteps, wearOffTime)
    {
        totalSize = size;
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

        UpdateSize(numberOfSteps);
    }

    private void UpdateSize(int numberOfSteps)
    {
        float sizeChange = numberOfSteps * sizeChangePerStep;

        remainingSize -= sizeChange;
        poweredUpBall.DecreaseSizeModifier(sizeChange);
    }
}
