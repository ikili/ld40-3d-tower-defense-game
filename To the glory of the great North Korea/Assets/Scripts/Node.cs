using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	public Color hoverColor;
	public Color cantBuildColor;
	[Header("Optional")]
	public GameObject turret;

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

		buildManager.BuildTurretOn(this);
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
