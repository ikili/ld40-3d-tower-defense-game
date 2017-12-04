using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public SceneFade sceneFade;
	public string mainMenuSceneName = "MainMenu";

	public GameObject ui;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("p"))
		{
			Toggle();
		}
	}

	public void Toggle()
	{
		ui.SetActive(!ui.activeSelf);

		if (ui.activeSelf)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	public void Retry()
	{
		Toggle();
		sceneFade.FadeTo(SceneManager.GetActiveScene().name);
	}

	public void Menu()
	{
		Toggle();
		sceneFade.FadeTo(mainMenuSceneName);
	}
}
