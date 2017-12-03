using UnityEngine;

public class Shop : MonoBehaviour
{
	BuildManager buildManager;

	void Start()
	{
		buildManager = BuildManager.instance;
	}

	public void PurchaseOneGunTurret()
	{
		buildManager.SetTurretToBuild(buildManager.oneGunTurretPrefab);
	}

	public void PurchaseTwoGunTurret()
	{
		buildManager.SetTurretToBuild(buildManager.twoGunTurretPrefab);
	}

	public void PurchaseMissileLauncher()
	{
		buildManager.SetTurretToBuild(buildManager.missileLauncherPrefab);
	}
}
