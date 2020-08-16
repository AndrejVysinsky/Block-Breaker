using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerUpAddScore : PowerUp
{
    protected override void ActivatePowerUp(float modifier, Vector3 position)
    {
        base.ActivatePowerUp(modifier);

        foreach (GameObject gameObject in GameEventListeners.Instance.listeners)
        {
            ExecuteEvents.Execute<IScoreChangedEvent>(gameObject, null, (x, y) => x.OnScoreChanged(position, (int)modifier));
        }
    }
}
