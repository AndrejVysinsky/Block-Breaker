using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPaddleWidth : PowerUpWithDuration
{
    private Paddle paddle;

    protected override void Start()
    {
        base.Start();

        duration = 10.0f;
        numberOfSteps = 200;
        wearOffTime = 2.0f;
    }

    protected override void ActivatePowerUp(float newModifier)
    {
        if (paddle == null)
        {
            paddle = player.GetPaddle();
        }

        paddle.ChangeSizeBy(-remainingModifier);
        paddle.ChangeSizeBy(newModifier);

        base.ActivatePowerUp(newModifier);
    }

    protected override void UpdatePowerUp(float modifierChange)
    {
        base.UpdatePowerUp(modifierChange);

        paddle.ChangeSizeBy(-modifierChange);
    }

    public void OnBallInitialized(Ball ball)
    {
        if (IsExpired() == false)
        {
            paddle.ChangeSizeBy(remainingModifier);
        }
    }
}
