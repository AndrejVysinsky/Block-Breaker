using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpSpeed : PowerUp
{
    private float speedModifier = 2.0f;
    private float duration = 5.0f;
    private int numberOfSteps = 20;

    private Gradient gradient = new Gradient();
    private Color originalColor = new Color32(0, 167, 255, 255);
    private Color powerUpColor = new Color32(255, 130, 0, 255);

    /*
     * https://docs.unity3d.com/ScriptReference/Color.Lerp.html?_ga=2.120109064.1657794911.1595002912-325438407.1590147552
     * 
     * https://docs.unity3d.com/ScriptReference/Gradient.Evaluate.html
     */

    private List<PoweredUpBallSpeed> poweredUpBalls;

    protected override void Start()
    {
        base.Start();

        gradient.colorKeys = new GradientColorKey[2]
        {
            new GradientColorKey(originalColor, 0.0f),
            new GradientColorKey(powerUpColor, 1.0f)
        };

        poweredUpBalls = new List<PoweredUpBallSpeed>();
    }

    protected override void ActivatePowerUp(GameObject ballObject)
    {
        base.ActivatePowerUp(ballObject);

        var ball = ballObject.GetComponent<Ball>();

        //check if ball is already powered up
        var poweredUpBall = poweredUpBalls.SingleOrDefault(x => x.IsPoweredUp(ball));

        if (poweredUpBall == null)
        {
            var newPoweredUpBall = new PoweredUpBallSpeed(ball, duration, numberOfSteps, speedModifier, gradient);
            poweredUpBalls.Add(newPoweredUpBall);
        }
        else
        {
            poweredUpBall.RefreshPowerUp();
        }
    }

    private void Update()
    {
        if (poweredUpBalls.Count > 0)
        {
            poweredUpBalls.RemoveAll(x => x.IsExpired());
            poweredUpBalls.ForEach(x => x.UpdateTime(Time.deltaTime));
        }
    }
}
