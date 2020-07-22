using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IScoreChangeEvent : IEventSystemHandler
{
    void DisplayScoreChangeText(Vector3 position, int score);
}
