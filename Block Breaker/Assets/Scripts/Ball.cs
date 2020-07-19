using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float velocityX = 3f;
    [SerializeField] float velocityY = 15f;

    private Rigidbody2D myRigidBody2D;

    private float currentSpeedModifier = 1.0f;

    private void Awake()
    {
        //launch ball
        velocityX *= UnityEngine.Random.Range(-0.1f, 0.1f);
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myRigidBody2D.velocity = new Vector2(velocityX, velocityY);
    }

    public void IncreaseSpeedModifier(float modifier)
    {
        myRigidBody2D.velocity /= currentSpeedModifier;

        currentSpeedModifier += modifier;

        myRigidBody2D.velocity *= currentSpeedModifier;

        Debug.Log($"Increased speed modifier to: {currentSpeedModifier}");
    }

    public void DecreaseSpeedModifier(float modifier)
    {
        myRigidBody2D.velocity /= currentSpeedModifier;

        currentSpeedModifier -= modifier;

        myRigidBody2D.velocity *= currentSpeedModifier;

        Debug.Log($"Decreased speed modifier to: {currentSpeedModifier}");
    }
}
