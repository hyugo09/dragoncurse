using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleCollector : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem ps;

    List<ParticleSystem.Particle> particles;

    List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleTrigger()
    {
        int trigerredParticle = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        for (int i = 0; i < trigerredParticle; i++)
        {
            ParticleSystem.Particle p = particles[i];

            p.remainingLifetime = 0;
            Debug.Log("particle collected");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<PlayerControl>() != null)
        {
           PlayerHealth health = other.GetComponent<PlayerHealth>();
            health.Heal(0.1f);
        }

    }
}
