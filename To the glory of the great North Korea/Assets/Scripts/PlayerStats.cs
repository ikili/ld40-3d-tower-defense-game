using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public static int Money;
	public static int Lives;
	public static int WaveNumber;
	public static int HighScore;

	public int startMoney = 1000;
	public int startLives = 20;
	public int startWaveNumber = 0;
	public int startHighScore = 0;

	void Start()
	{
		Money = startMoney;
		Lives = startLives;
		WaveNumber = startWaveNumber;
		HighScore = startHighScore;
	}
}
