using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public static bool gameOver = false;

	public GameObject gameOverUI;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		Load();
		gameOver = false;
	}

	void Update()
	{
		/*if (Input.GetKeyDown("e"))
		{
			EndGame();
		}*/
		if (gameOver)
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
		if (PlayerPrefs.HasKey("HighScore"))
		{
			PlayerStats.HighScore = PlayerPrefs.GetInt("HighScore");
		}
		if (PlayerPrefs.HasKey("MasterVolume"))
		{
			GameSettings.MasterVolume = PlayerPrefs.GetFloat("MasterVolume");
		}
		if (PlayerPrefs.HasKey("ScrollSpeed"))
		{
			GameSettings.ScrollSpeed = PlayerPrefs.GetFloat("ScrollSpeed");
		}
		if (PlayerPrefs.HasKey("PanSpeed"))
		{
			GameSettings.PanSpeed = PlayerPrefs.GetFloat("PanSpeed");
		}
		/*
		Debug.Log("Wczytano HighScore " + PlayerStats.HighScore);
		Debug.Log("Wczytano MasterVolume " + GameSettings.MasterVolume);
		Debug.Log("Wczytano ScrollSpeed " + GameSettings.ScrollSpeed);
		Debug.Log("Wczytano PanSpeed " + GameSettings.PanSpeed);
		*/
	}

	public void Save()
	{
		if (PlayerStats.WaveNumber > PlayerStats.HighScore)
		{
			PlayerPrefs.SetInt("HighScore", PlayerStats.WaveNumber);
		}
		PlayerPrefs.SetFloat("MasterVolume", GameSettings.MasterVolume);
		PlayerPrefs.SetFloat("ScrollSpeed", GameSettings.ScrollSpeed);
		PlayerPrefs.SetFloat("PanSpeed", GameSettings.PanSpeed);
		/*
		Debug.Log("Zapisano HighScore " + PlayerStats.HighScore);
		Debug.Log("Zapisano MasterVolume " + GameSettings.MasterVolume);
		Debug.Log("Zapisano ScrollSpeed " + GameSettings.ScrollSpeed);
		Debug.Log("Zapisano PanSpeed " + GameSettings.PanSpeed);
		*/
	}

	void EndGame()
	{
		Save();
		gameOver = true;
		gameOverUI.SetActive(true);
	}
}
