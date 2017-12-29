using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header("Attributes")]
	public float panMargin = 10f;
	[Header("Clamp Values")]
	public float minX = 0f;
	public float maxX = 80f;
	public float minY = 10f;
	public float maxY = 80f;
	public float minZ = -10f;
	public float maxZ = 70f;
	[Header("Camera Fixed Positions")]
	public Vector3[] fixedPositions;

	private bool doMovement = true;
	private bool movingToPos = false;
	private Vector3 moveVelocity;
	private Vector3 targetPos;
	BuildManager buildManager;

	void Start()
	{
		buildManager = BuildManager.instance;
	}

	void Update()
	{
		if (GameManager.GameOver)
		{
			this.enabled = false;
			return;
		}
		if (Input.GetButtonDown("Freeze Camera"))
		{
			doMovement = !doMovement;
		}
		if (doMovement == false || Time.timeScale == 0)
		{
			return;
		}
		if (Input.GetMouseButton(1))
		{
			buildManager.UnselectTurretToBuild();
		}
		if (fixedPositions.Length >= 6)
		{
			if (Input.GetButtonDown("Camera Top"))
			{
				movingToPos = true;
				targetPos = fixedPositions[0];
			}
			if (Input.GetButtonDown("Camera Right"))
			{
				movingToPos = true;
				targetPos = fixedPositions[1];
			}
			if (Input.GetButtonDown("Camera Bottom"))
			{
				movingToPos = true;
				targetPos = fixedPositions[2];
			}
			if (Input.GetButtonDown("Camera Left"))
			{
				movingToPos = true;
				targetPos = fixedPositions[3];
			}
			if (Input.GetButtonDown("Camera Center"))
			{
				movingToPos = true;
				targetPos = fixedPositions[4];
			}
			if (Input.GetButtonDown("Camera Fit All"))
			{
				movingToPos = true;
				targetPos = fixedPositions[5];
			}
		}
		if (movingToPos)
		{
			transform.position = Vector3.Lerp(transform.position, targetPos, (GameSettings.PanSpeed / 10f) * Time.deltaTime);

			if (Vector3.Distance(transform.position, targetPos) <= 0.5f)
			{
				movingToPos = false;
			}
		}
		else
		{
			if (Input.GetButton("Move Forward") || Input.mousePosition.y >= Screen.height - panMargin)
			{
				transform.Translate(Vector3.forward * GameSettings.PanSpeed * Time.deltaTime, Space.World);
			}
			if (Input.GetButton("Move Backward") || Input.mousePosition.y <= panMargin)
			{
				transform.Translate(Vector3.back * GameSettings.PanSpeed * Time.deltaTime, Space.World);
			}
			if (Input.GetButton("Move Left") || Input.mousePosition.x <= panMargin)
			{
				transform.Translate(Vector3.left * GameSettings.PanSpeed * Time.deltaTime, Space.World);
			}
			if (Input.GetButton("Move Right") || Input.mousePosition.x >= Screen.width - panMargin)
			{
				transform.Translate(Vector3.right * GameSettings.PanSpeed * Time.deltaTime, Space.World);
			}

			float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
			Vector3 pos = transform.position;

			pos.y -= scroll * 50000 * Time.deltaTime;

			pos.x = Mathf.Clamp(pos.x, minX, maxX);
			pos.y = Mathf.Clamp(pos.y, minY, maxY);
			pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

			transform.position = Vector3.SmoothDamp(transform.position, pos, ref moveVelocity, GameSettings.ScrollSpeed);
		}
	}
}
