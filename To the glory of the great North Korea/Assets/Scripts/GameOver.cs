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
	public GameObject highScoreGO;

	private TextMeshProUGUI numberOfWavesText;
	private TextMeshProUGUI highScoreText;

	void OnEnable()
	{
		if (numberOfWavesText == null)
		{
			numberOfWavesText = numberOfWavesGO.GetComponent<TextMeshProUGUI>();
			StartCoroutine(AnimateText("numberOfWaves"));
		}
		if (PlayerStats.HighScore > 0)
		{
			highScoreGO.SetActive(true);
			highScoreText = highScoreGO.GetComponent<TextMeshProUGUI>();
			StartCoroutine(AnimateText("highScore"));
		}
		else
		{
			highScoreGO.SetActive(false);
		}
	}

	IEnumerator AnimateText(string textName)
	{
		numberOfWavesText.text = "0";
		if (textName == "highScore")
		{
			highScoreText.text = "0";
		}

		int wave = 0;

		yield return new WaitForSeconds(1.5f);

		if (textName == "numberOfWaves")
		{
			while (wave < PlayerStats.WaveNumber)
			{
				wave++;
				numberOfWavesText.text = wave.ToString();
				yield return new WaitForSeconds(0.05f);
			}
		}

		if (textName == "highScore")
		{
			while (wave < PlayerStats.HighScore)
			{
				wave++;
				highScoreText.text = wave.ToString();
				yield return new WaitForSeconds(0.05f);
			}
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
