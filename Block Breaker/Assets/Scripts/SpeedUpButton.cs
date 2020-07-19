using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpeedUpButton : MonoBehaviour, IPointerClickHandler, IBallEvent
{
    private Player player;
    private Image image;
    private bool selected = false;

    private Color activeColor = new Color32(255, 255, 255, 255);
    private Color inactiveColor = new Color32(123, 123, 123, 255);

    private float speedToAdd = 1.0f;

    void Start()
    {
        image = GetComponent<Image>();
        player = FindObjectOfType<Player>();
    }

    private void OnLevelWasLoaded(int n)
    {
        /*
         * po zmene sceny sa neresetnu atributy (selected, image.color)
         * a zaroven su priradene stare referencie pre image a player???
         */

        player = FindObjectOfType<Player>();
        image = GetComponent<Image>();
        image.color = inactiveColor;
        selected = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selected = !selected;
        image.color = selected ? activeColor : inactiveColor;

        if (selected)
        {
            player.IncreaseSpeedModifier(speedToAdd);
        }
        else
        {
            player.DecreaseSpeedModifier(speedToAdd);
        }
    }

    public void OnBallInitialized(Ball ball)
    {
        if (selected)
        {
            ball.IncreaseSpeedModifier(speedToAdd);
        }
        Debug.Log("Event called!!!!");
    }
}
