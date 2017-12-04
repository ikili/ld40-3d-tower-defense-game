using System.Collections;
using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
	public GameObject livesGO;
	private TextMeshProUGUI livesText;

	void Start()
	{
		livesText = livesGO.GetComponent<TextMeshProUGUI>();
	}

	void Update()
	{
		if (PlayerStats.Lives <= 0)
		{
			livesText.text = "LIVES: 0";
		}
		else
		{
			livesText.text = "LIVES: " + PlayerStats.Lives.ToString();
		}

	}
}