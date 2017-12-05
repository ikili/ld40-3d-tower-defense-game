using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public SceneFade sceneFade;
	public string mainMenuSceneName = "MainMenu";

	public GameObject gameOverMenu;
	public GameObject optionsMenu;
	public GameObject pauseMenu;

	void Update()
	{
		if (gameOverMenu.activeSelf == false)
		{
			if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("p"))
			{
				Toggle();
			}
		}
		else
		{
			Time.timeScale = 1f;
			pauseMenu.SetActive(false);
		}
	}

	public void Toggle()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);

		if (pauseMenu.activeSelf)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	public void Options()
	{
		optionsMenu.SetActive(true);
		pauseMenu.SetActive(false);
	}

	public void Menu()
	{
		Toggle();
		sceneFade.FadeTo(mainMenuSceneName);
	}
}
