using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopItem
{
	public GameObject prefab;
	public GameObject upgradedPrefab;
	public int cost;
	public int upgradeCost;
	public Text priceText;

	public int GetSellAmount()
	{
		return cost / 2;
	}

	public int GetSellUpgradedAmount()
	{
		return (cost + upgradeCost) / 2;
	}
}
