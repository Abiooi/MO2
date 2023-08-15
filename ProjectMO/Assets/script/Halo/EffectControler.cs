using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public ParticleSystem[] effectParticleSystems;

    public void PlayEffects()
    {
        foreach (ParticleSystem particleSystem in effectParticleSystems)
        {
            particleSystem.Play();
        }
    }

    public void StopEffects()
    {
        foreach (ParticleSystem particleSystem in effectParticleSystems)
        {
            particleSystem.Stop();
        }
    }
}


