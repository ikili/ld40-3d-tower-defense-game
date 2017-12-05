using UnityEngine;

public class GameSettings
{
	public static float MasterVolume;
	public static float ScrollSpeed;
	public static float PanSpeed;

	public float startMasterVolume = 1f;
	public float startScrollSpeed = 150f;
	public float startPanSpeed = 50f;

	void Start()
	{
		MasterVolume = startMasterVolume;
		ScrollSpeed = startScrollSpeed;
		PanSpeed = startPanSpeed;
	}
}
