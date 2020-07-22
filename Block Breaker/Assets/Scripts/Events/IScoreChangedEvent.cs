using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IScoreChangedEvent : IEventSystemHandler
{
    /// <param name="position">Position of object that triggered score change</param>
    /// <param name="score">Amount of changed score</param>
    void OnScoreChanged(Vector3 position, int score);
}
