using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowFieldParticles : MonoBehaviour
{
    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;

    public float moveSpeed;
    void Start()
    {
        if (m_System == null)
        { m_System = GetComponent<ParticleSystem>(); }

        if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
        { m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles]; }


    }

    void Update()
    {
        int particleAmount = m_System.GetParticles(m_Particles);

        for (int i = 0; i < m_Particles.Length; i++)
        {
            ParticleSystem.Particle p = m_Particles[i];

            //Vector3 directionToTarget = (target.position - p.position).normalized;

            //Vector3 seekForce = (directionToTarget * force) * Time.deltaTime;

            //Vector3 seekForce = (Vector3.Normalize(noiseDirection) * force) * Time.deltaTime;
            //p.velocity += seekForce;

            Vector3 seekForce = transform.forward * moveSpeed * Time.deltaTime;

            p.velocity += seekForce;

            ////p.velocity += forceField;

            //m_Particles[i] = p;
        }

        m_System.SetParticles(m_Particles, particleAmount);

    }

    public void ApplyRotation(Vector3 rotation, float rotateSpeed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(rotation.normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    

}


