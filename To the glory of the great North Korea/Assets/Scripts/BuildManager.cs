using UnityEngine;

public class BuildManager : MonoBehaviour
{
	public static BuildManager instance;
	public GameObject oneGunTurretPrefab;
	public GameObject twoGunTurretPrefab;

	private GameObject turretToBuild;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
		}
		instance = this;
	}

	public GameObject GetTurretToBuild()
	{
		return turretToBuild;
	}

	public void SetTurretToBuild(GameObject turret)
	{
		turretToBuild = turret;
	}
}
