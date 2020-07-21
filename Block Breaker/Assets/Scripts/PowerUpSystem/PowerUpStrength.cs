using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PowerUpStrength : PowerUp
{
    private float strengthModifier = 5.0f;
    private float duration = 5.0f;
    private int numberOfSteps = 5;
    private float wearOffTime = 1.0f;

    private List<PoweredUpBallStrength> poweredUpBalls;

    protected override void Start()
    {
        base.Start();

        poweredUpBalls = new List<PoweredUpBallStrength>();
    }

    protected override void ActivatePowerUp(GameObject ballObject)
    {
        base.ActivatePowerUp(ballObject);

        var ball = ballObject.GetComponent<Ball>();

        //check if ball is already powered up
        var poweredUpBall = poweredUpBalls.SingleOrDefault(x => x.IsPoweredUp(ball));

        if (poweredUpBall == null)
        {
            var newPoweredUpBall = new PoweredUpBallStrength(ball, duration, numberOfSteps, wearOffTime, strengthModifier);
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