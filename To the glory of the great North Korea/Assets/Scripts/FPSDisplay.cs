using System.Collections;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
	float fps;
	float deltaTime = 0f;

	IEnumerator Start()
	{
		while (true)
		{
			fps = (1 / Time.deltaTime);
			deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
			yield return new WaitForSeconds(0.1f);
		}
	}

	void OnGUI()
	{
		float msec = deltaTime * 1000f;

		if (fps > 60)
		{
			GUI.color = Color.green;
		}
		else if (fps <= 60 && fps > 30)
		{
			GUI.color = Color.yellow;
		}
		else if (fps <= 30)
		{
			GUI.color = Color.red;
		}

		string label = string.Format("{0:0.0} ms ({1:0} fps)", msec, fps);

		Color oldColor = GUI.color;
		GUI.backgroundColor = Color.black;
		GUI.Box(new Rect(5, 5, 120f, 25f), label);
		GUI.backgroundColor = oldColor;
	}
}
