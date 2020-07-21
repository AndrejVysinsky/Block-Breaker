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

    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public virtual void Collect(GameObject gameObject)
    {
        if (gameObject.tag != "Ball")
        {
            return;
        }

        ActivatePowerUp(gameObject);
    }

    protected virtual void ActivatePowerUp(GameObject gameObject)
    {
    }

}
