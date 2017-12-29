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
	private float countdown;
	private TextMeshProUGUI waveCountdownText;
	private TextMeshProUGUI waveNumberText;
	private bool finishedSpawningWave = true;
	private bool setCountdown = true;

	public static int EnemiesSpawned = 0;
	public static int EnemiesKilled = 0;

	void Start()
	{
		waveCountdownText = waveCountdownGO.GetComponent<TextMeshProUGUI>();
		waveNumberText = waveNumberGO.GetComponent<TextMeshProUGUI>();
		countdown = timeBetweenWaves;
	}

	void Update()
	{
		countdown -= Time.deltaTime;
		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
		waveCountdownText.text = string.Format("{0:00.00}", countdown);
		waveNumberText.text = "WAVE: " + (PlayerStats.WaveNumber).ToString();
		if (EnemiesAlive > 0 || finishedSpawningWave == false)
		{
			return;
		}
		else
		{
			if (setCountdown == false)
			{
				countdown = timeBetweenWaves;
				setCountdown = true;
			}
		}
		if (countdown <= 0f)
		{
			setCountdown = false;
			/*if (EnemiesSpawned != EnemiesKilled)
			{
				Debug.Log("EnemiesSpawned: " + EnemiesSpawned + " EnemiesKilled: " + EnemiesKilled);
			}*/
			PlayerStats.WaveNumber++;
			if (PlayerStats.WaveNumber > waves.Length)
			{
				difficulty = ((Mathf.Pow(1.01f, (float)(3 * PlayerStats.WaveNumber)) / (float)(10 - GameSettings.Difficulty)) + 1);
			}
			StartCoroutine(SpawnWave());
			return;
		}
	}

	IEnumerator SpawnWave()
	{
		finishedSpawningWave = false;
		int randomWave = 1;

		if (PlayerStats.WaveNumber > waves.Length)
		{
			while (randomWave == previousWaveIndex)
			{
				randomWave = Random.Range(5, waves.Length);
			}
			previousWaveIndex = randomWave;
		}
		else
		{
			randomWave = PlayerStats.WaveNumber - 1;
		}
		if (PlayerStats.WaveNumber > 3 && GameSettings.Difficulty > 6)
		{
			randomWave = Random.Range(6, waves.Length);
		}
		if (PlayerStats.WaveNumber <= 3 && GameSettings.Difficulty > 6)
		{
			randomWave = PlayerStats.WaveNumber + 2;
		}

		customWave.enemies = new GameObject[waves[randomWave].enemies.Length];
		customWave.enemies = (GameObject[])waves[randomWave].enemies.Clone();
		customWave.rate = waves[randomWave].rate;
		customWave.count = waves[randomWave].count;
		customWave.count += (int)(PlayerStats.WaveNumber / 4 * difficulty);
		customWave.count = Mathf.Clamp(customWave.count, 6, 100);
		if (PlayerStats.WaveNumber > waves.Length)
		{
			customWave.rate = 2 * difficulty;
		}
		customWave.rate = Mathf.Clamp(customWave.count, 1, 10);

		int randomSpawnPoint;
		int randomEnemyToSpawn;

		for (int i = 0; i < customWave.count; i++)
		{
			randomSpawnPoint = Random.Range(0, spawnPoints.Length);
			randomEnemyToSpawn = Random.Range(0, customWave.enemies.Length);

			SpawnEnemy(randomSpawnPoint, customWave.enemies[randomEnemyToSpawn]);
			yield return new WaitForSeconds(1f / customWave.rate);
		}

		finishedSpawningWave = true;
		/*
		Debug.Log("Difficulty = " + difficulty);
		Debug.Log("EnemiesSpawned = " + enemiesSpawned);
		Debug.Log("EnemiesSpawnedThisWave = " + enemiesSpawnedThisWave);
		*/
	}

	void SpawnEnemy(int randomSpawnPoint, GameObject enemy)
	{
		GameObject enemyGO = Instantiate(enemy, new Vector3(spawnPoints[randomSpawnPoint].position.x, -0.45f, spawnPoints[randomSpawnPoint].position.z), spawnPoints[randomSpawnPoint].rotation);
		Enemy e = enemyGO.GetComponent<Enemy>();
		e.startNodeID = randomSpawnPoint;
		e.startHealth *= (float)(PlayerStats.WaveNumber / 10) * difficulty + 0.9f;
		EnemiesAlive++;
		EnemiesSpawned++;
	}
}
