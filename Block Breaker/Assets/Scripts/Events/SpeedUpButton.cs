using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpeedUpButton : MonoBehaviour, IPointerClickHandler, IBallInitializedEvent
{
    private Player player;
    private Image image;
    private bool selected = false;

    private Color activeColor = new Color32(255, 255, 255, 255);
    private Color inactiveColor = new Color32(123, 123, 123, 255);

    private float speedModifier = 1.0f;

    void Start()
    {
        image = GetComponent<Image>();
        player = FindObjectOfType<Player>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selected = !selected;
        image.color = selected ? activeColor : inactiveColor;

        if (selected)
        {
            player.IncreaseSpeedModifier(speedModifier);
        }
        else
        {
            player.DecreaseSpeedModifier(speedModifier);
        }
    }

    public void OnBallInitialized(Ball ball)
    {
        if (selected)
        {
            ball.IncreaseSpeedModifier(speedModifier);
        }
    }
}
