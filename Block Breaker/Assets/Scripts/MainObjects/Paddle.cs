using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float screenWidthInUnits = 32f;

    private Vector2 paddlePos;

    void Start()
    {
        paddlePos = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        //position in units
        float mousePosX = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        paddlePos.x = Mathf.Clamp(mousePosX, minX, maxX);
        transform.position = paddlePos;
    }
}
