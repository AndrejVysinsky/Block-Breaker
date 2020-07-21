using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class PoweredUpBall
{
    protected float totalDuration;
    protected int totalSteps;
    protected float timeForStep;
    protected float currentStepTime;
    protected float wearOffStartTime;

    protected float remainingDuration;
    protected int remainingSteps;

    protected Ball poweredUpBall;

    public PoweredUpBall(Ball ball, float duration, int numberOfSteps, float wearOffTime)
    {
        poweredUpBall = ball;

        totalDuration = duration;
        totalSteps = numberOfSteps;
        wearOffStartTime = wearOffTime;

        timeForStep = wearOffStartTime / numberOfSteps;
    }

    public virtual void RefreshPowerUp()
    {
        remainingDuration = totalDuration;
        remainingSteps = totalSteps;
        currentStepTime = 0;
    }

    public void UpdateTime(float deltaTime)
    {
        remainingDuration -= deltaTime;

        if (remainingDuration <= wearOffStartTime)
        {      
            currentStepTime += deltaTime;

            int numberOfSteps = (int)(currentStepTime / timeForStep);

            if (numberOfSteps > 0)
            {
                UpdatePowerUp(numberOfSteps);
            }
        }
    }

    protected virtual void UpdatePowerUp(int numberOfSteps)
    {
        remainingSteps -= numberOfSteps;
        currentStepTime -= numberOfSteps * timeForStep;
    }

    public virtual bool IsExpired()
    {
        return remainingDuration <= 0 || poweredUpBall == null;
    }

    public virtual bool IsPoweredUp(Ball ball)
    {
        return poweredUpBall == ball;
    }
}