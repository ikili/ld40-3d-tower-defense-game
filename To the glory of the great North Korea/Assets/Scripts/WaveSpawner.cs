using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
	[HideInInspector]
	public static int EnemiesAlive = 0;

	[Header("Custom Wave attributes")]
	[HideInInspector]
	public float difficulty = 1f;
	private WaveTypes customWave = new WaveTypes();

	[Header("Wave Spawner attributes")]
	public WaveTypes[] waves;
	public Transform[] spawnPoints;
	public float timeBetweenWaves = 2f;
	public GameObject waveCountdownGO;
	public GameObject waveNumberGO;

	private int previousWaveIndex = -1;
	private float countdown = 2f;
	private TextMeshProUGUI waveCountdownText;
	private TextMeshProUGUI waveNumberText;

	private int enemiesSpawned = 0;
	private int enemiesSpawnedThisWave = 0;

	void Start()
	{
		waveCountdownText = waveCountdownGO.GetComponent<TextMeshProUGUI>();
		waveNumberText = waveNumberGO.GetComponent<TextMeshProUGUI>();
	}

	void Update()
	{
		countdown -= Time.deltaTime;
		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
		waveCountdownText.text = string.Format("{0:00.00}", countdown);
		waveNumberText.text = "WAVE: " + PlayerStats.WaveNumber.ToString();
		if (EnemiesAlive > 0)
		{
			return;
		}
		if (countdown <= 0f)
		{
			if (PlayerStats.WaveNumber > waves.Length)
			{
				difficulty = ((Mathf.Pow(1.06f, (float)PlayerStats.WaveNumber) / (float)(10 - GameSettings.Difficulty)) + 1);
			}
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
			return;
		}
	}

	IEnumerator SpawnWave()
	{
		enemiesSpawnedThisWave = 0;

		int randomWave = 1;

		if (PlayerStats.WaveNumber > waves.Length)
		{
			while (randomWave == previousWaveIndex)
			{
				randomWave = Random.Range(0, waves.Length);
			}
			previousWaveIndex = randomWave;
		}
		else
		{
			randomWave = PlayerStats.WaveNumber - 1;
		}

		customWave.enemies = new GameObject[waves[randomWave].enemies.Length];
		customWave.enemies = (GameObject[])waves[randomWave].enemies.Clone();
		customWave.rate = waves[randomWave].rate;
		if (PlayerStats.WaveNumber <= waves.Length)
		{
			customWave.count = waves[randomWave].count;
		}
		else
		{
			customWave.count = (int)(6 * difficulty);
		}

		int randomSpawnPoint;
		int randomEnemyToSpawn;

		for (int i = 0; i < customWave.count; i++)
		{
			randomSpawnPoint = Random.Range(0, spawnPoints.Length);
			randomEnemyToSpawn = Random.Range(0, customWave.enemies.Length);

			SpawnEnemy(randomSpawnPoint, customWave.enemies[randomEnemyToSpawn]);
			yield return new WaitForSeconds(1f / customWave.rate);
		}
		/*
		Debug.Log("Difficulty = " + difficulty);
		Debug.Log("EnemiesSpawned = " + enemiesSpawned);
		Debug.Log("EnemiesSpawnedThisWave = " + enemiesSpawnedThisWave);
		*/
		PlayerStats.WaveNumber++;
	}

	void SpawnEnemy(int randomSpawnPoint, GameObject enemy)
	{
		GameObject enemyGO = Instantiate(enemy, spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);
		Enemy e = enemyGO.GetComponent<Enemy>();
		e.startNodeID = randomSpawnPoint;
		e.startHealth *= (difficulty * 0.85f);
		EnemiesAlive++;
		enemiesSpawnedThisWave++;
		enemiesSpawned++;
	}
}
