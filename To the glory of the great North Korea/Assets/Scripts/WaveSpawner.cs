using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
	public Transform enemyPrefab;
	public Transform[] spawnPoints;
	public float timeBetweenWaves = 5f;
	public GameObject nextWaveGO;

	private float countdown = 2f;
	private int waveIndex = 0;
	private TextMeshProUGUI nextWaveText;

	void Start()
	{
		nextWaveText = nextWaveGO.GetComponent<TextMeshProUGUI>();
	}

	void Update()
	{
		timeBetweenWaves = 5f + Mathf.Ceil(waveIndex / 2);
		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;
		nextWaveText.text = "NEXT WAVE IN: " + Mathf.Floor(countdown).ToString();
	}

	IEnumerator SpawnWave()
	{
		waveIndex++;
		int random;
		for (int i = 0; i < waveIndex; i++)
		{
			random = Random.Range(0, spawnPoints.Length);
			SpawnEnemy(random);
			yield return new WaitForSeconds(Random.Range(0.2f, 0.8f));
		}
	}

	void SpawnEnemy(int random)
	{
		Instantiate(enemyPrefab, spawnPoints[random].position, spawnPoints[random].rotation);
	}
}
