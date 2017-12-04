using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header("Attributes")]
	public float panSpeed = 30f;
	public float panMargin = 10f;
	public float scrollSpeed = 100f;
	public float dampTime = 0.3f;
	[Header("Clamp Values")]
	public float minX = 0f;
	public float maxX = 80f;
	public float minY = 10f;
	public float maxY = 80f;
	public float minZ = -10f;
	public float maxZ = 70f;

	private bool doMovement = true;
	private Vector3 moveVelocity;
	BuildManager buildManager;

	void Start()
	{
		buildManager = BuildManager.instance;
	}

	void Update()
	{
		if (GameManager.gameOver)
		{
			this.enabled = false;
			return;
		}
		if (Input.GetKeyDown("."))
		{
			doMovement = !doMovement;
		}
		if (doMovement == false)
		{
			return;
		}
		if (Input.GetMouseButton(1))
		{
			buildManager.UnselectTurretToBuild();
		}
		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panMargin)
		{
			transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("s") || Input.mousePosition.y <= panMargin)
		{
			transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("a") || Input.mousePosition.x <= panMargin)
		{
			transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panMargin)
		{
			transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");
		Vector3 pos = transform.position;

		pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;

		pos.x = Mathf.Clamp(pos.x, minX, maxX);
		pos.y = Mathf.Clamp(pos.y, minY, maxY);
		pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

		transform.position = Vector3.SmoothDamp(transform.position, pos, ref moveVelocity, dampTime);
	}
}
