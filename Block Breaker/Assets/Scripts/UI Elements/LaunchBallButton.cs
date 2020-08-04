using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaunchBallButton : MonoBehaviour, IPointerClickHandler
{
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        player.LaunchBall();
    }
}
