using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAddBall : PowerUp
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void ActivatePowerUp(GameObject ball)
    {
        base.ActivatePowerUp(ball);

        level.InstantiateBall(ball.transform.position);
    }
}
