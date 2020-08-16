using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PowerUpStrength : PowerUpWithDuration, IBallInitializedEvent
{
    private Gradient gradient = new Gradient();
    private Color originalColor = new Color32(184, 231, 255, 255);
    private Color powerUpColor = new Color32(233, 102, 102, 255);

    protected override void Start()
    {
        base.Start();

        modifier = 3.0f;
        duration = 15.0f;
        numberOfSteps = 1;
        wearOffTime = 1.0f;

        gradient.colorKeys = new GradientColorKey[2]
        {
            new GradientColorKey(originalColor, 0.0f),
            new GradientColorKey(powerUpColor, 1.0f)
        };
    }

    protected override void ActivatePowerUp(float newModifier)
    {
        player.GetBalls().ForEach(x =>
        {
            x.DecreaseStrengthModifierBy((int)remainingModifier);
            x.IncreaseStrengthModifierBy((int)newModifier);
            x.SetColor32(gradient.Evaluate(1));
            x.SetTrailColor32(gradient.Evaluate(1));
        });

        base.ActivatePowerUp(newModifier);
    }

    protected override void UpdatePowerUp(float modifierChange)
    {
        base.UpdatePowerUp(modifierChange);

        player.GetBalls().ForEach(x =>
        {
            x.DecreaseStrengthModifierBy((int)modifierChange);

            float colorValue = remainingModifier / modifier;
            x.SetColor32(gradient.Evaluate(colorValue));
            x.SetTrailColor32(gradient.Evaluate(colorValue));
        });
    }

    public void OnBallInitialized(Ball ball)
    {
        if (IsExpired() == false)
        {
            ball.IncreaseStrengthModifierBy((int)remainingModifier);

            float colorValue = remainingModifier / modifier;
            ball.SetColor32(gradient.Evaluate(colorValue));
            ball.SetTrailColor32(gradient.Evaluate(colorValue));
        }
    }
}