using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static bool gameEnded = false;

	void Update()
	{
		if (gameEnded)
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
		Debug.Log("Game Over!");
	}
}
