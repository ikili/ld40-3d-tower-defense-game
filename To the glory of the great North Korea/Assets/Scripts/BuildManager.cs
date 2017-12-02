using UnityEngine;

public class BuildManager : MonoBehaviour
{
	public static BuildManager instance;
	public GameObject oneGunTurretPrefab;

	private GameObject turretToBuild;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
		}
		instance = this;
	}

	void Start()
	{
		turretToBuild = oneGunTurretPrefab;
	}

	public GameObject GetTurretToBuild()
	{
		return turretToBuild;
	}
}
