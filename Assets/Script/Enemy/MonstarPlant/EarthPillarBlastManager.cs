using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPillarBlastManager : MonoBehaviour
{
    public int damage;
    ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        // int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        // Debug.Log(numEnter);
    }
}
