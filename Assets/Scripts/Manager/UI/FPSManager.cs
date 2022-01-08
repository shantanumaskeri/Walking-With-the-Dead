using UnityEngine;
using System.Collections;

public class FPSManager : MonoBehaviour
{

	private float deltaTime = 0.0f;

	public void incrementTime()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}

	public void showFpsOnScreen()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 0.02f);
		style.alignment = TextAnchor.UpperLeft;
		style.font = (Font)Resources.Load("Fonts/consolaz");
		style.fontSize = h * 2 / 35;
		style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms \n({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}
}