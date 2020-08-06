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

                Vector2 vector = GetRandomVelocityVector(baseVelocity);

                player.InstantiateBall(x.gameObject.transform.position);
                player.GetBalls().Last().SetVelocityVector(vector);
            }
        });
    }

    private Vector2 GetRandomVelocityVector(float baseVelocity)
    {
        float velX = Random.Range(0, baseVelocity);
        float velY = baseVelocity - velX;

        if (Random.Range(0f, 1f) <= 0.5)
        {
            velX = -velX;
        }

        if (Random.Range(0f, 1f) <= 0.5)
        {
            velY = -velY;
        }

        return new Vector2(velX, velY);
    }
}
