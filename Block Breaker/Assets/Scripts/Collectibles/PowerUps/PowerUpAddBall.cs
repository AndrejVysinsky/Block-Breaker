using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpAddBall : PowerUp
{
    protected override void ActivatePowerUp(float modifier)
    {
        base.ActivatePowerUp(modifier);

        List<Ball> balls = player.GetBalls().ToList();

        balls.ForEach(x =>
        {
            for (int i = 0; i < modifier; i++)
            {
                float baseVelocity = x.GetBaseVelocity();

                float velX = Random.Range(0, baseVelocity);
                float velY = baseVelocity - velX;

                Vector2 vector = new Vector2(velX, velY);

                player.InstantiateBall(x.gameObject.transform.position);
                player.GetBalls().Last().SetVelocityVector(vector);
            }
        });
    }
}
