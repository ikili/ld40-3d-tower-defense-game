using System.Collections;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
	private float deltaTime = 0f;

	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
	{
		float msec = deltaTime * 1000f;
		float fps = 1f / deltaTime;

		if (fps > 60)
		{
			GUI.color = Color.green;
		}
		else if (fps < 60)
		{
			GUI.color = Color.yellow;
		}
		else
		{
			GUI.color = Color.red;
		}

		string text = string.Format("{0:0.0} ms ({1:0} fps)", msec, fps);

		Color oldColor = GUI.color;
		GUI.backgroundColor = Color.black;
		GUI.Box(new Rect(5, 5, 120f, 25f), text);
		GUI.backgroundColor = oldColor;
	}
}
