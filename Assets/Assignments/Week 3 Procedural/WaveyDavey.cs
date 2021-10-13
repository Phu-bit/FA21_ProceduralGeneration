using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Art;

namespace phu
{

    public class WaveyDavey : ArtMakerTemplate
    {
        //////////////////////////////////////// noise stuff
        FastNoiseLite noise = new FastNoiseLite();
        public int noiseSize = 128;
        public float increment;
        public Vector3 offset, offsetSpeed;
        Vector3[] directionToTarget;
        Vector3[] positionArray;
        public int numObjects;


        float[] noisedata = new float[128 * 128];
        int index = 0;
        ///////////////////////////////////////////

        /////////////////////////////////////////// particle stuff
        ParticleSystem m_System;
        ParticleSystem.Particle[] m_Particles;
        public Transform attractor;
        public float forceAttract = 10.0f;
        public float repell = 10.0f;
        ///////////////////////////////////////////
        
        void InitializeIfNeeded()
        {
            Vector3[] directionToTarget = new Vector3[numObjects];
            Vector3[] positionArray = new Vector3[numObjects];

            for (int i = 0; i < directionToTarget.Length; i++)
            {
                Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0));
                Instantiate(attractor, screenPosition, Quaternion.identity);
                positionArray[i] = attractor.transform.position;

            }

            if (m_System == null)
            { m_System = GetComponent<ParticleSystem>(); }

            if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
            { m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles]; }

        }


        void LateUpdate()
        {
            InitializeIfNeeded();
            int particleAmount = m_System.GetParticles(m_Particles);
            Vector3 seekForce = new Vector3();

            for (int i = 0; i < m_Particles.Length; i++)
            {
                ParticleSystem.Particle p = m_Particles[i];




                //Vector3 noiseDirection = new Vector3(Mathf.Cos(noiseData * Mathf.PI), Mathf.Sin(noiseData * Mathf.PI), 0);
                //Vector3 seekForce = (Vector3.Normalize(noiseDirection) * force) * Time.deltaTime;

                directionToTarget[i] = (positionArray[i] - p.position).normalized;
                seekForce += (directionToTarget[i] * forceAttract) * Time.deltaTime;

                p.velocity += seekForce;


                //p.velocity += seekForce;

                //p.velocity += forceField;

                m_Particles[i] = p;
            }

            m_System.SetParticles(m_Particles, particleAmount);

        }


        void CreateNoise()
        {

        }

    }
}

