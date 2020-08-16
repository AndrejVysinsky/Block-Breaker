using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PowerUpShield : PowerUpWithDuration
{
    [SerializeField] GameObject shieldPrefab;

    private GameObject shield;

    protected override void Start()
    {
        base.Start();

        duration = 10.0f;
        numberOfSteps = 200;
        wearOffTime = 5.0f;
    }

    protected override void ActivatePowerUp(float newModifier)
    {
        if (shield == null)
        {
            shield = Instantiate(shieldPrefab);
        }

        ChangeSizeBy(-remainingModifier);
        ChangeSizeBy(newModifier);
        
        base.ActivatePowerUp(newModifier);
    }

    protected override void UpdatePowerUp(float modifierChange)
    {
        base.UpdatePowerUp(modifierChange);

        ChangeSizeBy(-modifierChange);
    }

    public void OnBallInitialized(Ball ball)
    {
        if (IsExpired() == false)
        {
            ChangeSizeBy(remainingModifier);
        }
    }

    private void ChangeSizeBy(float modifierChange)
    {
        Vector2 scale = shield.transform.localScale;

        scale.x += modifierChange;

        shield.transform.localScale = scale;
    }
}