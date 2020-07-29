using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    private float baseVelocity = 15f;
    private float maxVariation = 1.0f;

    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    private float speedModifier = 1.0f;
    private float sizeModifier = 1.0f;
    private int strengthModifier = 1;

    private int scorePenalty = -50;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();

        rigidBody2D.velocity = new Vector2(0, baseVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleVelocityChanges();
    }

    private void HandleVelocityChanges()
    {
        float velX = rigidBody2D.velocity.x;
        float velY = rigidBody2D.velocity.y;

        if (Math.Abs(velY) <= 2)
        {
            float newVelY = velY * 2;
            float newVelX = Math.Abs(velX) - Math.Abs(velY);

            if (velX < 0)
                newVelX = -newVelX;

            rigidBody2D.velocity = new Vector2(newVelX, newVelY);
        }

        float velocityVariation = baseVelocity * speedModifier - Math.Abs(velX) - Math.Abs(velY);

        if (Math.Abs(velocityVariation) > maxVariation)
        {
            if (velY < 0)
                velocityVariation = -velocityVariation;

            rigidBody2D.velocity = new Vector2(velX, velY + velocityVariation);
        }
    }

    public Vector2 GetVelocityVector()
    {
        return rigidBody2D.velocity;
    }

    public void SetVelocityVector(Vector2 vector)
    {
        Debug.Log($"Old velocity: {Math.Abs(rigidBody2D.velocity.x) + Math.Abs(rigidBody2D.velocity.y)}, new velocity: {Math.Abs(vector.x) + Math.Abs(vector.y)}");

        rigidBody2D.velocity = vector;
    }

    public void IncreaseSpeedModifier(float modifier)
    {
        ChangeSpeedModifierBy(modifier);
    }

    public void DecreaseSpeedModifier(float modifier)
    {
        ChangeSpeedModifierBy(-modifier);
    }

    private void ChangeSpeedModifierBy(float amount)
    {
        rigidBody2D.velocity /= speedModifier;

        speedModifier += amount;

        rigidBody2D.velocity *= speedModifier;
    }

    public void IncreaseSizeModifier(float modifier)
    {
        ChangeSizeModifierBy(modifier);
    }

    public void DecreaseSizeModifier(float modifier)
    {
        ChangeSizeModifierBy(-modifier);
    }

    private void ChangeSizeModifierBy(float amount)
    {
        transform.localScale /= sizeModifier;
        trailRenderer.startWidth /= sizeModifier;
        trailRenderer.endWidth /= sizeModifier;

        sizeModifier += amount;

        transform.localScale *= sizeModifier;
        trailRenderer.startWidth *= sizeModifier;
        trailRenderer.endWidth *= sizeModifier;
    }

    public void IncreaseStrengthModifier(int modifier)
    {
        strengthModifier += modifier;
    }

    public void DecreaseStrengthModifier(int modifier)
    {
        strengthModifier -= modifier;
    }

    public int GetStrength()
    {
        return strengthModifier;
    }

    public void SetColor32(Color32 color)
    {
        Color32 original = spriteRenderer.color;

        spriteRenderer.color = new Color32(color.r, color.g, color.b, original.a);
    }

    public void SetTrailColor32(Color32 color)
    {
        Color32 originalStart = trailRenderer.startColor;
        Color32 originalEnd = trailRenderer.endColor;

        trailRenderer.startColor = new Color32(color.r, color.g, color.b, originalStart.a);
        trailRenderer.endColor = new Color32(color.r, color.g, color.b, originalEnd.a);
    }
    
    public Gradient GetTrailGradient()
    {
        return trailRenderer.colorGradient;
    }

    public int GetScorePenalty()
    {
        return scorePenalty;
    }

    public void SendOutOfScreenMessage()
    {
        foreach (GameObject gameObject in GameEventListeners.Instance.listeners)
        {
            ExecuteEvents.Execute<IScoreChangedEvent>(gameObject, null, (x, y) => x.OnScoreChanged(transform.position, scorePenalty));
        }
    }
}
