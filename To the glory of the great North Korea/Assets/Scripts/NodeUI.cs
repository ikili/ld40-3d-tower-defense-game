using UnityEngine;
using TMPro;

public class NodeUI : MonoBehaviour
{
	public GameObject ui;
	public GameObject upgradeGO;
	public GameObject upgradeCostGO;
	public GameObject sellGO;
	public GameObject sellCostGO;

	private TextMeshProUGUI upgradeText;
	private TextMeshProUGUI upgradeCostText;
	private TextMeshProUGUI sellText;
	private TextMeshProUGUI sellCostText;
	private Node target;

	void Start()
	{
		upgradeText = upgradeGO.GetComponent<TextMeshProUGUI>();
		upgradeCostText = upgradeCostGO.GetComponent<TextMeshProUGUI>();
		sellText = sellGO.GetComponent<TextMeshProUGUI>();
		sellCostText = sellCostGO.GetComponent<TextMeshProUGUI>();
	}

	public void SetTarget(Node _target)
	{
		target = _target;

		transform.position = target.GetBuildPosition();

		if (_target.isUpgraded)
		{
			upgradeText.fontSizeMax = 14f;
			upgradeCostText.fontSizeMax = 14f;
			upgradeText.text = "NO UPGRADES";
			upgradeCostText.text = "AVAILABLE";
			sellText.text = "SELL";
			sellCostText.text = "₩" + target.shopItem.GetSellUpgradedAmount();
		}
		else
		{
			upgradeText.fontSizeMax = 18f;
			upgradeCostText.fontSizeMax = 18f;
			upgradeText.text = "UPGRADE";
			upgradeCostText.text = "₩" + target.shopItem.upgradeCost;
			sellText.text = "SELL";
			sellCostText.text = "₩" + target.shopItem.GetSellAmount();
		}

		ui.SetActive(true);
	}

	public void Hide()
	{
		ui.SetActive(false);
	}

	public void Upgrade()
	{
		target.UpgradeTurret();
		BuildManager.instance.DeselectNode();
	}

	public void Sell()
	{
		target.SellTurret();
		BuildManager.instance.DeselectNode();
	}
}
