using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public static bool GameOver = false;

	public GameObject gameOverUI;
	public GameObject musicControllerPrefab;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		Load();
		GameOver = false;
		if (MusicController.Instance == null)
		{
			Instantiate(musicControllerPrefab);
		}
		MusicController.Instance.startedPlaying = false;
	}

	void Update()
	{
		if (GameSettings.DebugMode)
		{
			if (Input.GetKeyDown("e"))
			{
				EndGame();
			}
		}
		if (GameOver)
		{
			return;
		}
		if (PlayerStats.Lives <= 0)
		{
			EndGame();
		}
	}

	public void Load()
	{
		PlayerStats.HighScore = PlayerPrefs.GetInt("HighScore", 0);
		GameSettings.MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
		GameSettings.ScrollSpeed = PlayerPrefs.GetFloat("ScrollSpeed", 150f);
		GameSettings.PanSpeed = PlayerPrefs.GetFloat("PanSpeed", 50f);
		GameSettings.Difficulty = PlayerPrefs.GetInt("Difficulty", 4);
		ValidateData();

		/*
		Debug.Log("Wczytano HighScore " + PlayerStats.HighScore);
		Debug.Log("Wczytano MasterVolume " + GameSettings.MasterVolume);
		Debug.Log("Wczytano ScrollSpeed " + GameSettings.ScrollSpeed);
		Debug.Log("Wczytano PanSpeed " + GameSettings.PanSpeed);
		*/
	}

	public void Save()
	{
		ValidateData();
		if (PlayerStats.WaveNumber > PlayerStats.HighScore)
		{
			
			PlayerPrefs.SetInt("HighScore", PlayerStats.WaveNumber);
		}
		else if (PlayerPrefs.HasKey("HighScore") == false)
		{
			PlayerPrefs.SetInt("HighScore", 0);
		}
		PlayerPrefs.SetFloat("MasterVolume", GameSettings.MasterVolume);
		PlayerPrefs.SetFloat("ScrollSpeed", GameSettings.ScrollSpeed);
		PlayerPrefs.SetFloat("PanSpeed", GameSettings.PanSpeed);
		PlayerPrefs.SetInt("Difficulty", GameSettings.Difficulty);

		/*
		Debug.Log("Zapisano HighScore " + PlayerStats.HighScore);
		Debug.Log("Zapisano MasterVolume " + GameSettings.MasterVolume);
		Debug.Log("Zapisano ScrollSpeed " + GameSettings.ScrollSpeed);
		Debug.Log("Zapisano PanSpeed " + GameSettings.PanSpeed);
		*/
	}

	void ValidateData()
	{
		Mathf.Clamp(PlayerStats.HighScore, 1, Mathf.Infinity);
		Mathf.Clamp01(GameSettings.MasterVolume);
		Mathf.Clamp(GameSettings.ScrollSpeed, 0, 999);
		Mathf.Clamp(GameSettings.PanSpeed, 0, 999);
		Mathf.Clamp(GameSettings.Difficulty, 0, 9);
	}

	void EndGame()
	{
		Save();
		GameOver = true;
		WaveSpawner.EnemiesAlive = 0;
		gameOverUI.SetActive(true);
	}
}
