using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //cached references
    protected Player player;
    private SpriteRenderer mySpriteRenderer;
    private BoxCollider2D myBoxCollider2D;

    protected bool expiresImmediately;

    protected enum PowerUpState
    {
        IsWaiting,
        IsActive,
        IsExpired
    }

    protected PowerUpState powerUpState;

    protected virtual void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<Player>();

        powerUpState = PowerUpState.IsWaiting;
    }

    public virtual void Collect(GameObject gameObjectCollectingPowerUp)
    {
        if (gameObjectCollectingPowerUp.tag != "Ball")
        {
            return;
        }

        ActivatePowerUp(gameObjectCollectingPowerUp);
    }

    protected virtual void ActivatePowerUp(GameObject gameObjectCollectingPowerUp)
    {
        mySpriteRenderer.enabled = false;
        myBoxCollider2D.enabled = false;
        powerUpState = PowerUpState.IsActive;

        if (expiresImmediately)
        {
            PowerUpHasExpired();
        }
    }

    protected virtual void PowerUpHasExpired()
    {
        powerUpState = PowerUpState.IsExpired;
        DestroyAfterDelay();
    }

    protected virtual void DestroyAfterDelay()
    {
        Destroy(gameObject, 10f);
    }
}
