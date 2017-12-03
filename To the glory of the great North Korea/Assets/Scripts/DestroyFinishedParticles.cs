using UnityEngine;

public class DestroyFinishedParticles : MonoBehaviour
{
	private ParticleSystem thisParticleSystem;

	void Start()
	{
		thisParticleSystem = GetComponent<ParticleSystem>();
	}

	void Update()
	{
		if (thisParticleSystem.isPlaying)
		{
			return;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
