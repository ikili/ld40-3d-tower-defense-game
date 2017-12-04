using UnityEngine;

public class BuildManager : MonoBehaviour
{
	public static BuildManager instance;
	public NodeUI nodeUI;
	public GameObject buildParticle;

	private ShopItem turretToBuild;
	private Node selectedNode;

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
		DeselectNode();
	}

	public ShopItem GetTurretToBuild()
	{
		return turretToBuild;
	}

	public void SelectNode(Node node)
	{
		if (selectedNode == node)
		{
			DeselectNode();
			return;
		}

		selectedNode = node;
		turretToBuild = null;

		nodeUI.SetTarget(node);
	}

	public void DeselectNode()
	{
		selectedNode = null;
		nodeUI.Hide();
	}

	public void UnselectTurretToBuild()
	{
		turretToBuild = null;
	}
}
