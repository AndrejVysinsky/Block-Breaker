using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ScoreTextScript : MonoBehaviour
{
    public string DisplayScore { get; set; } = "0";

    private TextMeshProUGUI scoreText;
    private float totalTime = 2.5f;
    private float speed = 0.5f;

    private float timer;

    private void Start()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
        timer = totalTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Destroy(gameObject);
        }

        scoreText.text = DisplayScore;
        scoreText.color = new Color32(255, 255, 255, (byte)(255 * (timer / totalTime)));
        
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
    }
}