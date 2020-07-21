using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PowerUpStrength : PowerUp
{
    [SerializeField] ParticleSystem strengthParticles;

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
            var particles = InstantiateParticles(ball);

            var newPoweredUpBall = new PoweredUpBallStrength(ball, duration, numberOfSteps, wearOffTime, strengthModifier, particles);

            poweredUpBalls.Add(newPoweredUpBall);
        }
        else
        {
            poweredUpBall.RefreshPowerUp();
        }
    }

    private ParticleSystem InstantiateParticles(Ball ball)
    {
        ParticleSystem particles = Instantiate(strengthParticles, ball.transform.position, ball.transform.rotation);

        ColorOverLifetimeModule colorOverLifetime = particles.colorOverLifetime;
        colorOverLifetime.color = new MinMaxGradient(ball.GetTrailGradient());

        particles.Play();

        particles.transform.parent = ball.transform;

        return particles;
    }

    private void Update()
    {
        if (poweredUpBalls.Count > 0)
        {
            var expired = poweredUpBalls.FindAll(x => x.IsExpired());
            
            expired.ForEach(x => Destroy(x.GetParticles()));

            poweredUpBalls.RemoveAll(x => expired.Contains(x));
            poweredUpBalls.ForEach(x => x.UpdateTime(Time.deltaTime));
        }
    }
}