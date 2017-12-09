using UnityEngine;

[System.Serializable]
public class WaveTypes
{
	public GameObject[] enemies;
	[HideInInspector]
	public int count = 6;
	public float rate;
}
