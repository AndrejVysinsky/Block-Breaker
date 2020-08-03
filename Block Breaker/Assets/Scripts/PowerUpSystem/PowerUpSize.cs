using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpSize : PowerUpWithDuration, IBallInitializedEvent
{
    protected override void Start()
    {
        base.Start();
        
        modifier = 2.0f;
        duration = 10.0f;
        numberOfSteps = 20;
        wearOffTime = 2.0f;
    }

    protected override void ActivatePowerUp(float modifier)
    {
        List<Ball> balls = player.GetBalls();

        balls.ForEach(x => x.DecreaseSizeModifierBy(remainingModifier));
        balls.ForEach(x => x.IncreaseSizeModifierBy(modifier));

        base.ActivatePowerUp(modifier);
    }

    protected override void UpdatePowerUp(float modifierChange)
    {
        base.UpdatePowerUp(modifierChange);

        player.GetBalls().ForEach(x => x.DecreaseSizeModifierBy(modifierChange));
    }

    public void OnBallInitialized(Ball ball)
    {
        if (remainingModifier > 0)
        {
            ball.IncreaseSizeModifierBy(remainingModifier);
        }
    }
}
