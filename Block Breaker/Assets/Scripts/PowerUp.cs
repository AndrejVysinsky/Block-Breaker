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

    //state variables
    private bool expiresImmediately;

    protected void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
    }

    public void Collect(GameObject gameObjectCollectingPowerUp)
    {
        if (gameObjectCollectingPowerUp.tag != "Ball")
        {
            return;
        }

        ActivatePowerUp(gameObjectCollectingPowerUp);
    }

    public virtual void ActivatePowerUp(GameObject gameObjectCollectingPowerUp)
    {
    }
}
