using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (!IsPointerOverGameObject())
        {
            float mousePosX = Input.mousePosition.x / Screen.width * screenWidthInUnits;
            paddlePos.x = Mathf.Clamp(mousePosX, minX, maxX);
            transform.position = paddlePos;
        }
    }

    private bool IsPointerOverGameObject()
    {
        // Check mouse
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }

        // Check touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).point.y >= transform.position.y)
        {
            BounceOffBall(collision);
        }
    }

    private void BounceOffBall(Collision2D collision)
    {
        var ball = collision.gameObject.GetComponent<Ball>();

        float ballX = collision.transform.position.x;

        float paddleLength = GetComponent<BoxCollider2D>().size.x;
        float paddleX = transform.position.x - paddleLength / 2;

        float velocityX = TranslateInterval(paddleX, paddleX + paddleLength, -10, 10, ballX);


        var oldVector = ball.GetVelocityVector();
        var totalSpeed = Math.Abs(oldVector.x) + Math.Abs(oldVector.y);

        var remainingSpeed = totalSpeed - Math.Abs(velocityX);
        var newVector = new Vector2(velocityX, remainingSpeed);

        ball.SetVelocityVector(newVector);
    }

    private float TranslateInterval(float oldMin, float oldMax, float newMin, float newMax, float oldValue)
    {
        return (((oldValue - oldMin) * (newMax - newMin)) / (oldMax - oldMin)) + newMin;
    }
}
