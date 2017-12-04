using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
	[HideInInspector]
	public static int EnemiesAlive = 0;

	[Header("Custom Wave attributes")]
	public float hardness = 1f;
	public float deltaHardness = 0.25f;
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

	void Start()
	{
		waveCountdownText = waveCountdownGO.GetComponent<TextMeshProUGUI>();
		waveNumberText = waveNumberGO.GetComponent<TextMeshProUGUI>();
	}

	void Update()
	{
		if (EnemiesAlive > 0)
		{
			return;
		}
		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			hardness += deltaHardness;
			countdown = timeBetweenWaves;
			return;
		}
		countdown -= Time.deltaTime;
		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
		waveCountdownText.text = string.Format("{0:00.00}", countdown);
		waveNumberText.text = "WAVE: " + (PlayerStats.WaveNumber + 1).ToString();
	}

	IEnumerator SpawnWave()
	{
		int randomWave = 1;
		while (randomWave == previousWaveIndex)
		{
			randomWave = Random.Range(0, waves.Length);
		}
		previousWaveIndex = randomWave;

		customWave.enemies = new GameObject[waves[randomWave].enemies.Length];
		customWave.enemies = (GameObject[])waves[randomWave].enemies.Clone();

		customWave.rate = waves[randomWave].rate;
		customWave.count = waves[randomWave].count;
		customWave.count = (int)(customWave.count * hardness);

		/*Debug.Log("Count: " + customWave.count);
		Debug.Log("Hardness: " + hardness);*/

		int randomSpawnPoint;
		int randomEnemyToSpawn;

		for (int i = 0; i < customWave.count; i++)
		{
			randomSpawnPoint = Random.Range(0, spawnPoints.Length);
			randomEnemyToSpawn = Random.Range(0, customWave.enemies.Length);

			SpawnEnemy(randomSpawnPoint, customWave.enemies[randomEnemyToSpawn]);
			yield return new WaitForSeconds(1f / customWave.rate);
		}

		PlayerStats.WaveNumber++;
	}

	void SpawnEnemy(int randomSpawnPoint, GameObject enemy)
	{
		GameObject enemyGO = Instantiate(enemy, spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);
		Enemy e = enemyGO.GetComponent<Enemy>();
		e.startNodeID = randomSpawnPoint;
		EnemiesAlive++;
	}
}
