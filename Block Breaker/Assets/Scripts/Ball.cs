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

    void Start()
    {
        //launch ball
        velocityX *= UnityEngine.Random.Range(-1f, 1f);
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myRigidBody2D.velocity = new Vector2(velocityX, velocityY);
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (isRegularSpeed)
            {
                SetSpeed(2);
                isRegularSpeed = false;
            }
        }            
        else
        {
            if (!isRegularSpeed)
            {
                SetSpeed(0.5f);
                isRegularSpeed = true;
            }
        }
    }

    public void SetSpeed(float scale)
    {
        float velX = myRigidBody2D.velocity.x;
        float velY = myRigidBody2D.velocity.y;

        myRigidBody2D.velocity = new Vector2(velX * scale, velY * scale);
    }
}
