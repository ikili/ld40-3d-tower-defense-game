using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
	public Transform enemyPrefab;
	public Transform[] spawnPoints;
	public float timeBetweenWaves = 5f;
	public GameObject waveCountdownGO;
	public GameObject waveNumberGO;

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
		timeBetweenWaves = 5f + Mathf.Ceil(PlayerStats.WaveNumber / 2);
		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;
		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
		waveCountdownText.text = "NEXT IN: " + Mathf.Floor(countdown).ToString();
		waveNumberText.text = "WAVE: " + PlayerStats.WaveNumber.ToString();
	}

	IEnumerator SpawnWave()
	{
		PlayerStats.WaveNumber++;
		int random;
		for (int i = 0; i < PlayerStats.WaveNumber; i++)
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
