using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    private Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        level.RemoveBall(collision.gameObject.GetComponent<Ball>());
    }
}
