using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class Collectible : MonoBehaviour, ICollectible
{
    private float value;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;

    private bool isColliding;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        rigidbody2D.velocity = new Vector2(0f, -5f);

        isColliding = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding)
            return;

        if (collision.gameObject.CompareTag("Paddle"))
        {
            Collected();
            isColliding = true;
        }

        if (collision.gameObject.CompareTag("LoseCollider"))
        {
            Missed();
            isColliding = true;
        }
    }

    public void SetValue(float valueToSet)
    {
        value = valueToSet;
    }

    public void SetSprite(Sprite spriteToSet)
    {
        spriteRenderer.sprite = spriteToSet;
    }

    public float GetCollectibleValue()
    {
        return value;
    }

    public void Collected()
    {
        foreach (GameObject gameObject in GameEventListeners.Instance.listeners)
        {
            ExecuteEvents.Execute<ICollectedEvent>(gameObject, null, (x, y) => x.OnCollected(this));
        }

        GetComponent<SpriteRenderer>().enabled = false;

        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        Despawn(audioSource.clip.length);
    }

    public void Missed()
    {
        foreach (GameObject gameObject in GameEventListeners.Instance.listeners)
        {
            ExecuteEvents.Execute<ICollectedEvent>(gameObject, null, (x, y) => x.OnMissed(this));
        }

        Despawn();
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

    public void Despawn(float timeToDespawn)
    {
        Destroy(gameObject, timeToDespawn);
    }
}
