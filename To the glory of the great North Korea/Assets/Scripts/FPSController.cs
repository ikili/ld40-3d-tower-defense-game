using UnityEngine;

public class FPSController : MonoBehaviour
{
	private bool showFPS = false;
	private FPSDisplay fpsDisplay;

	void Start()
	{
		fpsDisplay = GetComponent<FPSDisplay>();
	}

	void Update()
	{
		if (Input.GetButtonDown("Toggle FPS Counter"))
		{
			if (showFPS == false)
			{
				fpsDisplay.enabled = true;
			}
			else
			{
				fpsDisplay.enabled = false;
			}
			showFPS = !showFPS;
		}
	}
}
