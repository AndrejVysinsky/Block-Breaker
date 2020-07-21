using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float velocityX = 3f;
    [SerializeField] float velocityY = 15f;

    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    private float speedModifier = 1.0f;
    private float sizeModifier = 1.0f;
    private int strengthModifier = 1;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();

        LaunchBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Math.Abs(rigidBody2D.velocity.y) <= 1)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, rigidBody2D.velocity.y * 2);
        }
    }

    private void LaunchBall()
    {
        velocityX *= UnityEngine.Random.Range(-1f, 1f);
        rigidBody2D.velocity = new Vector2(velocityX, velocityY);
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
}
