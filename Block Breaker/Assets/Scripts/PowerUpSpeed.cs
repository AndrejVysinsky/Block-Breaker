using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : PowerUp
{
    private float speedModifier = 2.0f;
    private float speedChangeAmount;

    private Gradient powerUpGradient = new Gradient();

    /*
     * https://docs.unity3d.com/ScriptReference/Color.Lerp.html?_ga=2.120109064.1657794911.1595002912-325438407.1590147552
     * 
     * https://docs.unity3d.com/ScriptReference/Gradient.Evaluate.html
     */
    //prerobit
    private Color poweredUpColor = new Color(255, 167, 255, 255);
    private float[] colorChangeAmount = new float[3];
    private Color originalColor;

    private float powerUpDuration = 10.0f;
    private int powerUpChangeSteps = 20;
    private float timeForChangeStep;
    private float currentStepTime;
    
    private Ball poweredUpBall;

    protected override void Start()
    {
        base.Start();
        expiresImmediately = false;

        speedChangeAmount = speedModifier / powerUpChangeSteps;
        timeForChangeStep = powerUpDuration / powerUpChangeSteps;

    }

    protected override void ActivatePowerUp(GameObject ballObject)
    {
        base.ActivatePowerUp(ballObject);

        poweredUpBall = ballObject.GetComponent<Ball>();

        originalColor = poweredUpBall.GetColor();

        colorChangeAmount[0] = (poweredUpColor.r - originalColor.r) / powerUpChangeSteps;
        
        poweredUpBall.SetColor(poweredUpColor);
        poweredUpBall.SetTrailColor(poweredUpColor);
        poweredUpBall.IncreaseSpeedModifier(speedModifier);
    }

    private void Update()
    {
        if (powerUpState == PowerUpState.IsActive)
        {
            if (poweredUpBall == null || powerUpDuration <= 0)
            {
                PowerUpHasExpired();
            }

            powerUpDuration -= Time.deltaTime;
            
            currentStepTime += Time.deltaTime;
            int numberOfSteps = (int)(currentStepTime / timeForChangeStep);

            if (numberOfSteps > 0)
            {
                ChangeBallSpeed(numberOfSteps);
                //ChangeBallColor(numberOfSteps);
            }
        }
    }

    private void ChangeBallSpeed(int numberOfSteps)
    {
        powerUpChangeSteps -= numberOfSteps;
        currentStepTime -= numberOfSteps * timeForChangeStep;
        poweredUpBall.DecreaseSpeedModifier(numberOfSteps * speedChangeAmount);
    }

    private void ChangeBallColor(int numberOfSteps)
    {
        //TODO
        poweredUpBall.ChangeColorBy(-colorChangeAmount[0], -colorChangeAmount[1], -colorChangeAmount[2]);

        
    }

    protected override void PowerUpHasExpired()
    {
        //handle particles destroing, etc...

        base.PowerUpHasExpired();
    }
}
