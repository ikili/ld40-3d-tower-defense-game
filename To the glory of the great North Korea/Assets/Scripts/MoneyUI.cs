using System.Collections;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
	public GameObject moneyGO;

	private TextMeshProUGUI moneyText;

	void Start()
	{
		moneyText = moneyGO.GetComponent<TextMeshProUGUI>();
	}

	void Update()
	{
		moneyText.text = "KPW(₩): " + PlayerStats.Money.ToString();
	}
}
