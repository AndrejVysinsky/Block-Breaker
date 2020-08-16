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
        numberOfSteps = 20;
        wearOffTime = 2.0f;
    }

    protected override void ActivatePowerUp(float newModifier)
    {
        if (paddle == null)
        {
            paddle = player.GetPaddle();
        }

        paddle.DecreaseScaleXBy(remainingModifier);
        paddle.IncreaseScaleXBy(newModifier);

        base.ActivatePowerUp(newModifier);
    }

    protected override void UpdatePowerUp(float modifierChange)
    {
        base.UpdatePowerUp(modifierChange);

        paddle.DecreaseScaleXBy(modifierChange);
    }

    public void OnBallInitialized(Ball ball)
    {
        if (IsExpired() == false)
        {
            paddle.IncreaseScaleXBy(remainingModifier);
        }
    }
}
