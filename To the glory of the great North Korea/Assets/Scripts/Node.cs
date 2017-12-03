using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	public Color hoverColor;

	private Renderer r;
	private Color startColor;
	private GameObject turret;

	BuildManager buildManager;

	void Start()
	{
		r = GetComponent<Renderer>();
		startColor = r.material.color;

		buildManager = BuildManager.instance;
	}

	void OnMouseDown()
	{
		if (buildManager.GetTurretToBuild() == null || EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if (turret != null)
		{
			Debug.Log("Can't build there.");
			return;
		}

		GameObject turretToBuild = buildManager.GetTurretToBuild();
		turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
	}

	void OnMouseEnter()
	{
		if (buildManager.GetTurretToBuild() == null || EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}
		r.material.color = hoverColor;
	}

	void OnMouseExit()
	{
		r.material.color = startColor;
	}
}
