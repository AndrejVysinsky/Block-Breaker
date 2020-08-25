using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public interface ICollectedEvent : IEventSystemHandler
{
    void OnCollected(Collectible collectible);

    void OnMissed(Collectible collectible);
}
