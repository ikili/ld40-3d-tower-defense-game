using UnityEngine;

public class BuildManager : MonoBehaviour
{
	public static BuildManager instance;
	public GameObject buildParticle;

	private ShopItem turretToBuild;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
		}
		instance = this;
	}

	public bool CanBuild { get { return turretToBuild != null; } }

	public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

	public void SelectTurretToBuild(ShopItem turret)
	{
		turretToBuild = turret;
	}

	public void UnselectTurretToBuild()
	{
		turretToBuild = null;
	}

	public void BuildTurretOn(Node node)
	{
		if (PlayerStats.Money < turretToBuild.cost)
		{
			Debug.Log("Not enough money to build that!");
			return;
		}

		PlayerStats.Money -= turretToBuild.cost;

		Instantiate(buildParticle, node.GetBuildPosition(), Quaternion.identity);

		GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;
	}
}
