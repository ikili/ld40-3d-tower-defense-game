using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
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
		StartCoroutine(AnimateText());
	}

	IEnumerator AnimateText()
	{
		numberOfWavesText.text = "0";

		int wave = 0;

		yield return new WaitForSeconds(1.5f);

		while (wave < PlayerStats.WaveNumber)
		{
			wave++;
			numberOfWavesText.text = wave.ToString();
			yield return new WaitForSeconds(0.05f);
		}
	}

	public void Retry()
	{
		WaveSpawner.EnemiesAlive = 0;
		sceneFade.FadeTo(SceneManager.GetActiveScene().name);
	}

	public void Menu()
	{
		sceneFade.FadeTo(mainMenuSceneName);
	}
}
