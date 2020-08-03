using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PowerUpWithDuration : PowerUp
{
    protected float modifier = 2.0f;
    protected float duration = 10.0f;
    protected int numberOfSteps = 20;
    protected float wearOffTime = 2.0f;

    protected float remainingModifier = 0.0f;
    protected float remainingDuration = 0.0f;

    protected float timeForStep;
    protected float changePerStep;
    protected float currentStepTime;

    protected override void Start()
    {
        base.Start();
    }

    protected virtual void Update()
    {
        if (remainingDuration > 0)
        {
            remainingDuration -= Time.deltaTime;

            if (remainingDuration <= wearOffTime)
            {
                currentStepTime += Time.deltaTime;

                int numberOfSteps = (int)(currentStepTime / timeForStep);

                if (numberOfSteps > 0)
                {
                    UpdateSteps(numberOfSteps);
                }
            }
        }
    }

    protected virtual void UpdateSteps(int numberOfSteps)
    {
        currentStepTime -= numberOfSteps * timeForStep;

        float modifierChange = numberOfSteps * changePerStep;

        remainingModifier -= modifierChange;

        UpdatePowerUp(modifierChange);
    }

    protected override void ActivatePowerUp(float newModifier)
    {
        base.ActivatePowerUp(newModifier);

        modifier = newModifier;
        remainingModifier = modifier;
        remainingDuration = duration;

        timeForStep = wearOffTime / numberOfSteps;
        changePerStep = modifier / numberOfSteps;
        currentStepTime = 0f;
    }

    protected virtual void UpdatePowerUp(float modifierChange)
    {
    }
}
