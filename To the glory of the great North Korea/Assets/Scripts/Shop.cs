using UnityEngine;

public class Shop : MonoBehaviour
{
	public ShopItem oneGunTurret;
	public ShopItem twoGunTurret;
	public ShopItem missileLauncher;
	public ShopItem laserBeamer;

	BuildManager buildManager;

	void Start()
	{
		buildManager = BuildManager.instance;
		oneGunTurret.priceText.text = "₩" + oneGunTurret.cost.ToString();
		twoGunTurret.priceText.text = "₩" + twoGunTurret.cost.ToString();
		missileLauncher.priceText.text = "₩" + missileLauncher.cost.ToString();
		laserBeamer.priceText.text = "₩" + laserBeamer.cost.ToString();
	}

	public void SelectOneGunTurret()
	{
		buildManager.SelectTurretToBuild(oneGunTurret);
	}

	public void SelectTwoGunTurret()
	{
		buildManager.SelectTurretToBuild(twoGunTurret);
	}

	public void SelectMissileLauncher()
	{
		buildManager.SelectTurretToBuild(missileLauncher);
	}

	public void SelectLaserBeamer()
	{
		buildManager.SelectTurretToBuild(laserBeamer);
	}
}
