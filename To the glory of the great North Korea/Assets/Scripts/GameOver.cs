using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
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
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Menu()
	{
		Debug.Log("Go to menu"); 
	}
}
