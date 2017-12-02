using UnityEngine;

public class Node : MonoBehaviour
{
	public Color hoverColor;

	private Renderer r;
	private Color startColor;
	private GameObject turret;

	void Start()
	{
		r = GetComponent<Renderer>();
		startColor = r.material.color;
	}

	void OnMouseDown()
	{
		if (turret != null)
		{
			Debug.Log("Can't build there.");
			return;
		}

		GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
		turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
	}

	void OnMouseEnter()
	{
		r.material.color = hoverColor;
	}

	void OnMouseExit()
	{
		r.material.color = startColor;
	}
}
