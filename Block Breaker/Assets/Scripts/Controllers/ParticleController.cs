using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public static ParticleController Instance { get; private set; }

    [SerializeField] ParticleSystem damageParticlesPrefab;

    private List<ParticleSystem> activeDamageParticles;

    private int maxActiveParticles = 10;

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
        if (activeDamageParticles == null)
        {
            activeDamageParticles = new List<ParticleSystem>();
        }
    }

    public ParticleSystem GetDamageParticles()
    {
        foreach(var activeParticles in activeDamageParticles)
        {
            if (activeParticles.isPlaying == false)
            {
                return activeParticles;
            }
        }

        ParticleSystem particles = Instantiate(damageParticlesPrefab, transform);

        activeDamageParticles.Add(particles);

        return particles;
    }
}
