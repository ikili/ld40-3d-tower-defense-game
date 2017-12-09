using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public SceneFade sceneFade;

	public GameObject mainMenu;
	public GameObject options;

	public string levelToLoad = "Main";

	void Start()
	{
		GameManager.Instance.Load();
		MusicController.Instance.startedPlaying = false;
	}

	public void StartGame()
	{
		WaveSpawner.EnemiesAlive = 0;
		sceneFade.FadeTo(levelToLoad);
	}

	public void Options()
	{
		options.SetActive(true);
		mainMenu.SetActive(false);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
