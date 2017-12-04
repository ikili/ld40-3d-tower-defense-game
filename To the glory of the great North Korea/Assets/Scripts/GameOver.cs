using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
	public SceneFade sceneFade;
	public string mainMenuSceneName = "MainMenu";
	public GameObject numberOfWavesGO;

	private TextMeshProUGUI numberOfWavesText;

	void OnEnable()
	{
		if (numberOfWavesText == null)
		{
			numberOfWavesText = numberOfWavesGO.GetComponent<TextMeshProUGUI>();
		}
		numberOfWavesText.text = PlayerStats.WaveNumber.ToString();
	}

	public void Retry()
	{
		sceneFade.FadeTo(SceneManager.GetActiveScene().name);
	}

	public void Menu()
	{
		sceneFade.FadeTo(mainMenuSceneName);
	}
}
