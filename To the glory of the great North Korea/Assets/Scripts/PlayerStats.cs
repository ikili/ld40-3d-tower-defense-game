using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public static int Money;
	public static int Lives;
	public static int WaveNumber;
	public static int HighScore;

	public int startMoney = 400;
	public int startLives = 10;
	public int startWaveNumber = 1;

	void Start()
	{
		Money = startMoney;
		Lives = startLives;
		WaveNumber = startWaveNumber;
	}
}
