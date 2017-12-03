using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	public Color hoverColor;
	public Color cantBuildColor;
	[HideInInspector]
	public GameObject turret;
	[HideInInspector]
	public ShopItem shopItem;
	[HideInInspector]
	public bool isUpgraded = false;

	private Renderer r;
	private Color startColor;

	BuildManager buildManager;

	void Start()
	{
		r = GetComponent<Renderer>();
		startColor = r.material.color;

		buildManager = BuildManager.instance;
	}

	public Vector3 GetBuildPosition()
	{
		return transform.position;
	}

	void BuildTurret(ShopItem item)
	{
		if (PlayerStats.Money < item.cost)
		{
			Debug.Log("Not enough money to build that!");
			return;
		}

		PlayerStats.Money -= item.cost;

		shopItem = item;

		Instantiate(buildManager.buildParticle, GetBuildPosition(), Quaternion.identity);

		GameObject _turret = (GameObject)Instantiate(item.prefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;
	}

	public void UpgradeTurret()
	{
		if (isUpgraded)
		{
			return;
		}

		if (PlayerStats.Money < shopItem.upgradeCost)
		{
			Debug.Log("Not enough money to upgrade that!");
			return;
		}

		PlayerStats.Money -= shopItem.upgradeCost;

		Destroy(turret);

		Instantiate(buildManager.buildParticle, GetBuildPosition(), Quaternion.identity);

		GameObject _turret = (GameObject)Instantiate(shopItem.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		isUpgraded = true;
	}

	void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if (turret != null)
		{
			buildManager.SelectNode(this);
		}

		if (!buildManager.CanBuild)
		{
			return;
		}

		BuildTurret(buildManager.GetTurretToBuild());
	}

	void OnMouseEnter()
	{
		if (!buildManager.CanBuild || EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}
		if (buildManager.HasMoney)
		{
			r.material.color = hoverColor;
		}
		else
		{
			r.material.color = cantBuildColor;
		}
	}

	void OnMouseExit()
	{
		r.material.color = startColor;
	}
}
