using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //cached references
    protected Level level;
    private SpriteRenderer mySpriteRenderer;

    //state variables
    private bool expiresImmediately;

    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
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

    public void SetLevelReference(Level levelRef)
    {
        level = levelRef;
    }

}
