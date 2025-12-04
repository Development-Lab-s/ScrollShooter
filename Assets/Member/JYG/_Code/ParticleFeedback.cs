using System.Collections.Generic;
using UnityEngine;

public class ParticleFeedback : MonoBehaviour
{
    public List<ParticleSystem> particleSystems;

    public void StartParticle()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }
    }

    public void PauseParticle()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Pause();
        }
    }
}
