using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Art;

[RequireComponent(typeof(ParticleSystem))]
public class WavyDavey2 : ArtMakerTemplate
{
	//select shape
	public enum typeOfShape { Box, Sphere, Hemisphere, Cone, Donut, Circle, Rectangle }
	public typeOfShape selectShape;
	public bool randomizeShape = false;
	public bool randomizeType = false;
    private int shapeNumber;
	

	//particlecolor and other settings
	public Vector3 offset;
	public Vector3 rotation;
	ParticleSystem.ColorOverLifetimeModule colorModule;
	Gradient ourGradientMin;
	Gradient ourGradientMax;
	ParticleSystemShapeType shapetype;
	public int maxNumParticles, minNumParticles, startLifeMin, startLifeMax;
	public Vector2 morphSpeedRange = new Vector2(0.01f, 1.5f);
	public Vector2 strengthRange = new Vector2(0.01f, 1.5f);
	public Vector2 frequencyRange = new Vector2(0.001f, 2f);

	public Vector2 octavesRange = new Vector2(1, 8);
	public Vector2 lacunarityRange = new Vector2(1f, 4f);
	public Vector2 persistenceRange = new Vector2(0f, 1f);
	public Vector2 dimensionsRange = new Vector2(1, 3f);
	public bool randomLac, randomOct, randomPers, randomDim;





	public float morphSpeed;

	[Range(0f, 1f)]
	public float strength = 1f;

	public bool damping;

	public float frequency = 1f;

	[Range(1, 8)]
	public int octaves = 1;

	[Range(1f, 4f)]
	public float lacunarity = 2f;

	[Range(0f, 1f)]
	public float persistence = 0.5f;

	[Range(1, 3)]
	public int dimensions = 3;

	public NoiseMethodType type;

	public int noiseType = 1;

	private ParticleSystem system;
	private ParticleSystem.Particle[] particles;

	private float morphOffset;

    private void Start()
    {
        if (randomLac)
        {
			lacunarity = Random.Range(lacunarityRange.x, lacunarityRange.y);
        }
		if (randomOct)
		{
			octaves = Mathf.RoundToInt(Random.Range(octavesRange.x, octavesRange.y));
		}
		if (randomPers)
		{
			persistence = Random.Range(persistenceRange.x, persistenceRange.y);
		}
		if (randomLac)
		{
			dimensions = Mathf.RoundToInt(Random.Range(dimensionsRange.x, dimensionsRange.y));
		}



		offset.x = Random.Range(0.01f, 0.33f);
		offset.y = offset.x + Random.Range(0.1f, 0.33f);
		offset.z = offset.y + Random.Range(0.1f, 0.33f);

		rotation.x = Random.Range(0.01f, 0.33f);
		rotation.y = rotation.x + Random.Range(0.1f, 0.33f);
		rotation.z = rotation.y + Random.Range(0.1f, 0.33f);

		morphSpeed = Random.Range(morphSpeedRange.x, morphSpeedRange.y);

		strength = Random.Range(strengthRange.x, strengthRange.y);

		frequency = Random.Range(frequencyRange.x, frequencyRange.y);

        if (randomizeType)
        {
			noiseType = Random.Range(0, 1);
			if (noiseType == 1)
			{
				type = NoiseMethodType.Value;
			}
			else
			{
				type = NoiseMethodType.Perlin;
			}
		}

		if (system == null)
		{
			system = GetComponent<ParticleSystem>();
		}

		//main
		var numParts = system.main;
		numParts.maxParticles = Random.Range(minNumParticles, maxNumParticles);
		numParts.startLifetime = Random.Range(startLifeMin, startLifeMax);

		//emission
		var Em = system.emission;
		Em.rateOverTime = numParts.maxParticles;

		if (particles == null || particles.Length < system.main.maxParticles)
		{
			particles = new ParticleSystem.Particle[system.main.maxParticles];
		}

		//gradient
		colorModule = system.colorOverLifetime;
		float alpha1 = 1.0f;
		ourGradientMin = new Gradient();
		ourGradientMin.SetKeys(
			new GradientColorKey[] { new GradientColorKey(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 0.0f), new GradientColorKey(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha1, 0.0f), new GradientAlphaKey(alpha1, 1.0f) }
		);

		// A simple 2 color gradient with a fixed alpha of 0.0f.
		float alpha2 = 0.0f;
		ourGradientMax = new Gradient();
		ourGradientMax.SetKeys(
			new GradientColorKey[] { new GradientColorKey(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 0.0f), new GradientColorKey(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha2, 0.0f), new GradientAlphaKey(alpha2, 1.0f) }
		);

		// Apply the gradients.
		colorModule.color = new ParticleSystem.MinMaxGradient(ourGradientMin, ourGradientMax);

		var sh = system.shape;
		if (randomizeShape){
			shapeNumber = Random.Range(0, 6);
		}
        else
        {
			shapeNumber = (int)selectShape;
        }

		if (shapeNumber == 0)
        {
			 shapetype = ParticleSystemShapeType.Box;
        }
        else if (shapeNumber == 1)
        {
			shapetype = ParticleSystemShapeType.Sphere;

		}
		else if (shapeNumber == 2)
		{
			shapetype = ParticleSystemShapeType.Hemisphere;

		}
		else if (shapeNumber == 3)
		{
			shapetype = ParticleSystemShapeType.Cone;

		}
		else if (shapeNumber == 4)
		{
			shapetype = ParticleSystemShapeType.Donut;

		}
		else if (shapeNumber == 5)
		{
			shapetype = ParticleSystemShapeType.Circle;

		}
		else if (shapeNumber == 6)
		{
			shapetype = ParticleSystemShapeType.Rectangle;

		}

		sh.shapeType = shapetype;
	}

	private void LateUpdate()
	{
		int particleCount = system.GetParticles(particles);
		MakeArt();
		system.SetParticles(particles, particleCount);
	}

	public override void MakeArt()
	{

		Quaternion q = Quaternion.Euler(rotation);
		Quaternion qInv = Quaternion.Inverse(q);
		NoiseMethod method = Noise.methods[(int)type][dimensions - 1];
		float amplitude = damping ? strength / frequency : strength;
		morphOffset += Time.deltaTime * morphSpeed;
		if (morphOffset > 256f)
		{
			morphOffset -= 256f;
		}
		for (int i = 0; i < particles.Length; i++)
		{
			Vector3 position = particles[i].position;
			Vector3 point = q * new Vector3(position.z, position.y, position.x + morphOffset) + offset;
			NoiseSample sampleX = Noise.Sum(method, point, frequency, octaves, lacunarity, persistence);
			sampleX *= amplitude;
			sampleX.derivative = qInv * sampleX.derivative;
			point = q * new Vector3(position.x + 100f, position.z, position.y + morphOffset) + offset;
			NoiseSample sampleY = Noise.Sum(method, point, frequency, octaves, lacunarity, persistence);
			sampleY *= amplitude;
			sampleY.derivative = qInv * sampleY.derivative;
			point = q * new Vector3(position.y, position.x + 100f, position.z + morphOffset) + offset;
			NoiseSample sampleZ = Noise.Sum(method, point, frequency, octaves, lacunarity, persistence);
			sampleZ *= amplitude;
			sampleZ.derivative = qInv * sampleZ.derivative;
			Vector3 curl;
			curl.x = sampleZ.derivative.x - sampleY.derivative.y;
			curl.y = sampleX.derivative.x - sampleZ.derivative.y ;//sampleX.derivative.x - sampleZ.derivative.y;
			curl.z = sampleY.derivative.x - sampleX.derivative.y + (1f / (1f + Mathf.Sin(position.z)));
			particles[i].velocity = curl;
		}
	}


}

