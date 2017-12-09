using UnityEngine;

public class LockRotation : MonoBehaviour
{
	private Quaternion rotation;

	void Awake()
	{
		rotation = transform.rotation;
	}

	void LateUpdate()
	{
		transform.rotation = rotation;
	}
}
