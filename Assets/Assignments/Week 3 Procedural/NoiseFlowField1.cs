using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFlowField1 : MonoBehaviour
{
    FastNoiseLite fastnoise;
    public Vector2[,] flowFieldDirection;
    public Vector3Int gridSize;
    public float cellSize;
    public float increment;
    public Vector3 offset, offsetSpeed;
    public float particleMoveSpeed, particleRotateSpeed;

    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;



    //float[] noisedata = new float[128 * 128];
    //int index = 0;

    private void Start()
    {
        flowFieldDirection = new Vector2 [gridSize.x, gridSize.y];
        fastnoise = new FastNoiseLite();

        if (m_System == null)
        { m_System = GetComponent<ParticleSystem>(); }

        if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
        { m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles]; }




        //var sh = m_System.shape;
        //sh.enabled = true;
        //sh.shapeType = ParticleSystemShapeType.Box;
        //sh.position = -transform.position + new Vector3(0, 0.3f, 15);
        //sh.scale = new Vector3(gridSize.x * cellSize, gridSize.y * cellSize, gridSize.z * cellSize);

    }

    private void Update()
    {
        CalculateFlowFieldDirections();
        int particleAmount = m_System.GetParticles(m_Particles);
        ParticleBehaviour();
        m_System.SetParticles(m_Particles, particleAmount);

    }

    void CalculateFlowFieldDirections()
    {
        //make noise grid

        fastnoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);

        float xOff = 0f;
        for (int y = 0; y < gridSize.x; y++)
        {
            float yOff = 0f;
            for (int x = 0; x < gridSize.y; x++)
            {
                float zOff = 0f;
                for (int z = 0; z < gridSize.z; z++)
                {
                    float noiseData = fastnoise.GetNoise(xOff + offset.x, y + offset.y, zOff + offset.z);
                    Vector3 noiseDirection = new Vector3(Mathf.Cos(noiseData * Mathf.PI), Mathf.Sin(noiseData * Mathf.PI), 0);
                    flowFieldDirection[x, y] = Vector3.Normalize(noiseDirection);


                    zOff += increment;
                }
                yOff += increment;
            }
            xOff += increment;
        }

    }

    void ParticleBehaviour()
    {
        
        for (int i = 0; i < m_Particles.Length; i++)
        {
            ParticleSystem.Particle p = m_Particles[i];

            

            //Vector3 rotation = flowFieldDirection[particlePos.x,particlePos.y];


            //p.velocity += seekForce;
            //p.velocity = Vector3.ClampMagnitude(p.velocity, 10);
            m_Particles[i] = p;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(this.transform.position + new Vector3((gridSize.x * cellSize) * 0.5f, (gridSize.z * cellSize) * 0.5f, (gridSize.y * cellSize) * 0.5f),
            new Vector3(gridSize.x * cellSize, gridSize.y * cellSize, gridSize.z * cellSize));
    }

}