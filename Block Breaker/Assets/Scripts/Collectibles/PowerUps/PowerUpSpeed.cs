using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpSpeed : PowerUpWithDuration, IBallInitializedEvent
{
    protected override void Start()
    {
        base.Start();

        modifier = 0.5f;
        duration = 10.0f;
        numberOfSteps = 20;
        wearOffTime = 3.0f;
    }

    protected override void ActivatePowerUp(float newModifier)
    {
        player.GetBalls().ForEach(x =>
        {
            x.DecreaseSpeedModifierBy(remainingModifier);
            x.IncreaseSpeedModifierBy(newModifier);
        });

        base.ActivatePowerUp(newModifier);
    }

    protected override void UpdatePowerUp(float modifierChange)
    {
        base.UpdatePowerUp(modifierChange);

        player.GetBalls().ForEach(x => x.DecreaseSpeedModifierBy(modifierChange));
    }
    
    public void OnBallInitialized(Ball ball)
    {
        if (IsExpired() == false)
        {
            ball.IncreaseSpeedModifierBy(remainingModifier);
        }
    }
}
