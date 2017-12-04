using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static bool gameOver = false;

	public GameObject gameOverUI;

	void Start()
	{
		gameOver = false;
	}

	void Update()
	{
		if (Input.GetKeyDown("e"))
		{
			EndGame();
		}
		if (gameOver)
		{
			return;
		}
		if (PlayerStats.Lives <= 0)
		{
			EndGame();
		}
	}

	void EndGame()
	{
		gameOver = true;
		gameOverUI.SetActive(true);
	}
}
