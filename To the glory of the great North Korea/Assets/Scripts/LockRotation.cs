using UnityEngine;

public class LockRotation : MonoBehaviour
{
	public float yRotation = 0f;
	private Quaternion rotation;

	void Awake()
	{
		rotation = transform.rotation;
	}

	void LateUpdate()
	{
		//transform.rotation = rotation;
		transform.rotation = Quaternion.Euler(new Vector3(rotation.x, yRotation, rotation.z));
	}
}
