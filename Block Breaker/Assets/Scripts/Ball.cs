using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();

        LaunchBall();
    }

    private void LaunchBall()
    {
        velocityX *= UnityEngine.Random.Range(-0.1f, 0.1f);
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

        Debug.Log($"Changed speed modifier by {amount} to {speedModifier}");
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

        sizeModifier += amount;

        transform.localScale *= sizeModifier;

        Debug.Log($"Changed size modifier by {amount} to {sizeModifier}");
    }

    public void ChangeColorBy(float r = 0, float g = 0, float b = 0, float a = 0)
    {
        Color color = spriteRenderer.color;

        color.r += r;
        color.g += g;
        color.b += b;
        color.a += a;

        Color color2 = trailRenderer.startColor;

        color2.r += r;
        color2.g += g;
        color2.b += b;
        color2.a += a;

        spriteRenderer.color = color;
        trailRenderer.startColor = color2;
        trailRenderer.endColor = color2;

        Debug.Log($"Color changed to {color}");
        Debug.Log($"Sprite changed to {color}");
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public Color GetColor()
    {
        return spriteRenderer.color;
    }

    public void SetTrailColor(Color color)
    {
        trailRenderer.startColor = color;
        trailRenderer.endColor = color;
    }

    public void SetTrailGradient(Gradient gradient)
    {
        trailRenderer.colorGradient = gradient;
    }
}
