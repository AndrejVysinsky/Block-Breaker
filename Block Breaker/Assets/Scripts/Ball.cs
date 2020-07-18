using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float velocityX = 3f;
    [SerializeField] float velocityY = 15f;

    private Rigidbody2D myRigidBody2D;
    private bool isRegularSpeed = true;

    private void Awake()
    {
        //launch ball
        velocityX *= UnityEngine.Random.Range(-1f, 1f);
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myRigidBody2D.velocity = new Vector2(velocityX, velocityY);
    }

    public void ToggleSpeed()
    {
        if (isRegularSpeed)
        {
            SetSpeed(2);
            isRegularSpeed = false;
        }
        else
        {
            SetSpeed(0.5f);
            isRegularSpeed = true;
        }
    }

    private void SetSpeed(float scale)
    {
        float velX = myRigidBody2D.velocity.x;
        float velY = myRigidBody2D.velocity.y;

        myRigidBody2D.velocity = new Vector2(velX * scale, velY * scale);
    }
}
