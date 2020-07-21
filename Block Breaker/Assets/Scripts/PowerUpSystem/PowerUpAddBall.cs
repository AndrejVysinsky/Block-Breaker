using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAddBall : PowerUp
{
    protected override void ActivatePowerUp(GameObject ballObject)
    {
        base.ActivatePowerUp(ballObject);

        player.InstantiateBall(ballObject.transform.position);
    }
}
