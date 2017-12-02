using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	public Transform enemyPrefab;
	public Transform[] spawnPoints;
	public float timeBetweenWaves = 5f;

	private float countdown = 2f;
	private int waveIndex = 0;

	void Update()
	{
		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;
	}

	IEnumerator SpawnWave()
	{
		waveIndex++;
		int random;
		Debug.Log("Wave incomming!");
		for (int i = 0; i < waveIndex; i++)
		{
			random = Random.Range(0, spawnPoints.Length);
			SpawnEnemy(random);
			yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
		}
	}

	void SpawnEnemy(int random)
	{
		Instantiate(enemyPrefab, spawnPoints[random].position, spawnPoints[random].rotation);
	}
}
