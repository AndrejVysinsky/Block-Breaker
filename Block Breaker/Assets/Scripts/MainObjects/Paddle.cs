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
    private Camera mainCamera;

    void Start()
    {
        paddlePos = new Vector2(transform.position.x, transform.position.y);

        mainCamera = Camera.main;
    }

    void Update()
    {
        //position in units
        if (!Level.Instance.IsPointerOverGameObject())
        {
            float mousePosX = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;

            paddlePos.x = Mathf.Clamp(mousePosX, minX, maxX);
            transform.position = paddlePos;
        }
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
        float paddleLength = GetComponent<BoxCollider2D>().size.x * transform.localScale.x;
        float paddleX = transform.position.x - paddleLength / 2;

        var ball = collision.gameObject.GetComponent<Ball>();
        float ballX = Mathf.Clamp(collision.transform.position.x, paddleX, paddleX + paddleLength);

        float velocityX = TranslateInterval(paddleX, paddleX + paddleLength, -12, 12, ballX);

        var oldVector = ball.GetVelocityVector();
        var totalSpeed = Mathf.Abs(oldVector.x) + Mathf.Abs(oldVector.y);

        var remainingSpeed = totalSpeed - Mathf.Abs(velocityX);
        var newVector = new Vector2(velocityX, remainingSpeed);

        ball.SetVelocityVector(newVector);
    }

    private float TranslateInterval(float oldMin, float oldMax, float newMin, float newMax, float oldValue)
    {
        float oldIntervalLength = oldMax - oldMin;
        float newIntervalLength = newMax - newMin;

        float ratio = (oldValue - oldMin) / oldIntervalLength;

        return ratio * newIntervalLength + newMin;
    }

    public void IncreaseScaleXBy(float amount)
    {
        ChangeScaleXBy(amount);
    }

    public void DecreaseScaleXBy(float amount)
    {
        ChangeScaleXBy(-amount);
    }

    private void ChangeScaleXBy(float amount)
    {
        Vector2 scale = transform.localScale;

        float originalMinX = minX;

        minX /= scale.x;
        scale.x += amount;
        minX *= scale.x;

        float difference = minX - originalMinX;

        maxX -= difference;

        transform.localScale = scale;
    }
}
