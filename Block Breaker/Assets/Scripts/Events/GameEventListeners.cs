using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameEventListeners : MonoBehaviour
{
    public static GameEventListeners Instance { get; private set; }

    public List<GameObject> listeners;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (listeners == null)
        {
            listeners = new List<GameObject>();
        }

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Listener");

        listeners.AddRange(gameObjects);
    }

    public void AddListener(GameObject gameObject)
    {
        if (!listeners.Contains(gameObject))
        {
            listeners.Add(gameObject);
        }
    }
}
