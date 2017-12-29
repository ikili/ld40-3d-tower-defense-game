using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
	public Slider volumeSlider;
	public Slider scrollSpeedSlider;
	public InputField panSpeedInputField;
	public InputField difficultyInputField;

	public GameObject otherMenu;
	public GameObject options;

	private bool startSetup = true;

	void Update()
	{
		if (options.activeSelf == true)
		{
			otherMenu.SetActive(false);
			if (startSetup)
			{
				GameManager.Instance.Load();
				volumeSlider.value = GameSettings.MasterVolume;
				scrollSpeedSlider.value = (1f - GameSettings.ScrollSpeed);
				Text panSpeedPlaceholder = panSpeedInputField.GetComponentInChildren<Text>();
				Text difficultyPlaceholder = difficultyInputField.GetComponentInChildren<Text>();
				panSpeedPlaceholder.text = "Current: " + GameSettings.PanSpeed + " Default: 100";
				difficultyPlaceholder.text = "Current: " + GameSettings.Difficulty + " Default: 5";
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
		MusicController.Instance.settingsChanged = true;
	}

	public void SetScrollSpeed(float value)
	{
		value = Mathf.Clamp01(value);
		value = 1f - value;
		GameSettings.ScrollSpeed = value;
	}

	public void SetPanSpeed(string str)
	{
		if (str == "")
		{
			return;
		}
		float value = float.Parse(str);
		GameSettings.PanSpeed = value;
	}

	public void SetDifficulty(string str)
	{
		if (str == "")
		{
			return;
		}
		int value = int.Parse(str);
		GameSettings.Difficulty = value;
	}

	public void Back()
	{
		startSetup = true;
		otherMenu.SetActive(true);
		options.SetActive(false);
		panSpeedInputField.Select();
		panSpeedInputField.text = "";
		difficultyInputField.Select();
		difficultyInputField.text = "";
		GameManager.Instance.Save();
	}
}
