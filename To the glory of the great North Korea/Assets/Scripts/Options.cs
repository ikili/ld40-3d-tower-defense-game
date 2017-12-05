using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
	public Slider volumeSlider;
	public InputField scrollSpeedInputField;
	public InputField panSpeedInputField;

	public GameObject mainMenu;
	public GameObject options;

	GameManager gameManager;

	private bool startSetup = true;

	void Update()
	{
		if (options.activeSelf == true)
		{
			mainMenu.SetActive(false);
			if (startSetup)
			{
				gameManager = GameManager.instance;
				gameManager.Load();
				volumeSlider.value = GameSettings.MasterVolume;
				startSetup = false;
			}
		}
	}

	public void ResetHighScores()
	{
		PlayerStats.HighScore = 0;
	}

	public void SetVolume(float value)
	{
		value = Mathf.Clamp01(value);
		GameSettings.MasterVolume = value;
	}

	public void SetScrollSpeed(string str)
	{
		float value = float.Parse(str);
		GameSettings.ScrollSpeed = value;
	}

	public void SetPanSpeed(string str)
	{
		float value = float.Parse(str);
		GameSettings.PanSpeed = value;
	}

	public void Back()
	{
		startSetup = true;
		mainMenu.SetActive(true);
		options.SetActive(false);
		GameManager.instance.Save();
	}
}
