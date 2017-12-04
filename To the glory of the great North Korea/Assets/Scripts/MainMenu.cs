using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public string levelToLoad = "Main";

	public void StartGame()
	{
		SceneManager.LoadScene(levelToLoad);
	}

	public void Options()
	{

	}

	public void Quit()
	{
		Application.Quit();
	}
}
