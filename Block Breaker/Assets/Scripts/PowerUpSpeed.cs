using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : PowerUp
{
    private float speedModifier = 2.0f;
    private int speedChangeSteps = 20;
    private float speedUpDurationInSeconds = 10.0f;

    private float timeForSpeedChangeStep;
    private float speedChangeAmount;

    private float currentTimeStep;
    
    private Ball poweredUpBall;

    protected override void Start()
    {
        base.Start();
        expiresImmediately = false;

        speedChangeAmount = speedModifier / speedChangeSteps;
        timeForSpeedChangeStep = speedUpDurationInSeconds / speedChangeSteps;
    }

    protected override void ActivatePowerUp(GameObject ballObject)
    {
        base.ActivatePowerUp(ballObject);

        poweredUpBall = ballObject.GetComponent<Ball>();
        poweredUpBall.IncreaseSpeedModifier(speedModifier);
    }

    private void Update()
    {
        if (powerUpState == PowerUpState.IsActive)
        {
            if (poweredUpBall == null || speedUpDurationInSeconds <= 0)
            {
                PowerUpHasExpired();
            }

            speedUpDurationInSeconds -= Time.deltaTime;
            
            currentTimeStep += Time.deltaTime;
            int numberOfSteps = (int)(currentTimeStep / timeForSpeedChangeStep);

            if (numberOfSteps > 0)
            {
                ChangeBallSpeed(numberOfSteps);
                ChangeBallColor(numberOfSteps);
            }
        }
    }

    private void ChangeBallSpeed(int numberOfSteps)
    {
        speedChangeSteps -= numberOfSteps;
        currentTimeStep -= numberOfSteps * timeForSpeedChangeStep;
        poweredUpBall.DecreaseSpeedModifier(numberOfSteps * speedChangeAmount);
    }

    private void ChangeBallColor(int numberOfSteps)
    {
        //TODO
    }

    protected override void PowerUpHasExpired()
    {
        //handle particles destroing, etc...

        base.PowerUpHasExpired();
    }
}
