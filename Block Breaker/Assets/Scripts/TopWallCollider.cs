using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopWallCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var image = GameObject.FindGameObjectWithTag("MainUI").GetComponent<RectTransform>();
        var imageSize = image.sizeDelta;

        imageSize.y /= 30;

        Debug.Log($"Image size: {imageSize}");

        var collider = GetComponent<BoxCollider2D>();

        Debug.Log($"Collider size: {collider.size}");

        collider.size = imageSize;

        
    }
}
