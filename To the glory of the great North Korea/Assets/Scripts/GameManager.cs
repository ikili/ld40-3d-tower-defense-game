using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

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
		if (Input.GetButtonDown("Toggle GameOver"))
		{
			EndGame();
		}
		if (Input.GetButtonDown("Mute BG Music"))
		{
			if (MusicController.Instance.bgMusicSource.isPlaying)
			{
				MusicController.Instance.bgMusicSource.Pause();
			}
			else
			{
				MusicController.Instance.bgMusicSource.UnPause();
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

	public void updateAnalytics()
	{
		if (GameOver)
		{
			Analytics.CustomEvent("PlayerStats", new Dictionary<string, object>
				{
					{ "WaveNumber", PlayerStats.WaveNumber },
					{ "HighScore", PlayerStats.HighScore },
					{ "Money", PlayerStats.Money },
					{ "FinalDifficulty", GameSettings.Difficulty },
					{ "PlayTime(s)", (int)Time.unscaledTime }
				});	
		}
	}

	public void Load()
	{
		PlayerStats.HighScore = PlayerPrefs.GetInt("HighScore", 0);
		GameSettings.MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
		GameSettings.ScrollSpeed = PlayerPrefs.GetFloat("ScrollSpeed", 0.15f);
		GameSettings.PanSpeed = PlayerPrefs.GetFloat("PanSpeed", 100f);
		GameSettings.Difficulty = PlayerPrefs.GetInt("Difficulty", 5);
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
			PlayerStats.HighScore = PlayerStats.WaveNumber;
			PlayerPrefs.SetInt("HighScore", PlayerStats.HighScore);
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
		Mathf.Clamp01(GameSettings.ScrollSpeed);
		Mathf.Clamp(GameSettings.PanSpeed, 0, 999);
		Mathf.Clamp(GameSettings.Difficulty, 0, 9);
	}

	void EndGame()
	{
		Save();
		GameOver = true;
		updateAnalytics();
		gameOverUI.SetActive(true);
	}
}
