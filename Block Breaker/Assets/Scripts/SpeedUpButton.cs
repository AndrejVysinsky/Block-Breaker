using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpeedUpButton : MonoBehaviour, IPointerClickHandler
{
    private Level level;
    private Image image;
    private bool selected = false;

    private Color activeColor = new Color32(255, 255, 255, 255);
    private Color inactiveColor = new Color32(123, 123, 123, 255);

    void Start()
    {
        level = FindObjectOfType<Level>();
        image = GetComponent<Image>();
        //image.color = inactiveColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selected = !selected;
        image.color = selected ? activeColor : inactiveColor;
        level.ToggleBallsSpeed();
    }
}
