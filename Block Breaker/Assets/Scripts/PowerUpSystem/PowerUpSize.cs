using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpSize : PowerUp
{
    private float sizeModifier = 2.0f;
    private float duration = 10.0f;
    private int numberOfSteps = 20;
    private float wearOffTime = 2.0f;

    private List<PoweredUpBallSize> poweredUpBalls;

    protected override void Start()
    {
        base.Start();

        poweredUpBalls = new List<PoweredUpBallSize>();
    }

    protected override void ActivatePowerUp(GameObject ballObject)
    {
        base.ActivatePowerUp(ballObject);

        var ball = ballObject.GetComponent<Ball>();

        //check if ball is already powered up
        var poweredUpBall = poweredUpBalls.SingleOrDefault(x => x.IsPoweredUp(ball));

        if (poweredUpBall == null)
        {
            var newPoweredUpBall = new PoweredUpBallSize(ball, duration, numberOfSteps, wearOffTime, sizeModifier);
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
            poweredUpBalls.RemoveAll(x => x.IsExpired() || x.IsDestroyed());
            poweredUpBalls.ForEach(x => x.UpdateTime(Time.deltaTime));
        }
    }
}
