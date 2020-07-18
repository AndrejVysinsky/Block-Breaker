using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAddBall : PowerUp
{
    public override void ActivatePowerUp(GameObject ball)
    {
        base.ActivatePowerUp(ball);

        player.InstantiateBall(ball.transform.position);
    }
}
