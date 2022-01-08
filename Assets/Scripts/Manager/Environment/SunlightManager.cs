using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SunlightManager : MonoBehaviour
{

	[Header("Sunlight Settings")]
	public Transform sunTransform;
	public Light sun;

	[Header("Fog Color")]
	public Color fogDay = Color.grey;
	public Color fogNight = Color.black;

	private float time;
	private float intensity;

	private string fullDate;
	private string fullTime;
	private string timeSuffix;
	private string daySuffix;

	public void getTime()
	{
		float minutes = float.Parse(System.DateTime.Now.ToString("mm"));
		float hours = float.Parse(System.DateTime.Now.ToString("HH"));
		float seconds = (hours * 3600.0f) + (minutes * 60.0f);

		time = seconds;

		fullTime = System.DateTime.Now.ToString("HH:mm");
		timeSuffix = System.DateTime.Now.AddHours(0).ToString("tt");
	}

	public void getDate()
	{
		string dayOfWeek = System.DateTime.Today.DayOfWeek.ToString();
		string month = System.DateTime.Now.ToString("MMMM");
		string dayOfMonth = System.DateTime.Today.Day.ToString();
		string year = System.DateTime.Today.Year.ToString();

		switch (dayOfMonth)
		{
			case "1":
			case "21":
			case "31":
				daySuffix = "st";
				break;

			case "2":
			case "22":
				daySuffix = "nd";
				break;

			case "3":
			case "23":
				daySuffix = "rd";
				break;

			default:
				daySuffix = "th";
				break;
		}

		fullDate = dayOfWeek + " " + month + " " + dayOfMonth + daySuffix + ", " + year;
	}

	public void changeLightIntensity()
	{
		if (time < 43200)
		{
			intensity = 1 - ((43200 - time) / 43200);
		}
		else
		{
			intensity = 1 - ((43200 - time) / 43200 * -1);
		}

		sun.intensity = intensity;
		sunTransform.rotation = Quaternion.Euler(new Vector3((time - 21600) / 86400 * 360, 0, 0));
	}

	public void animateFogColor()
	{
		RenderSettings.fogColor = Color.Lerp(fogNight, fogDay, intensity * intensity);
	}

	public void showDateAndTimeOnScreen()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 0.02f);
		style.alignment = TextAnchor.UpperCenter;
		style.font = (Font)Resources.Load("Fonts/consolaz");
		style.fontSize = h * 2 / 35;
		style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		string label = fullDate + "\n" + fullTime + " " + timeSuffix;
		GUI.Label(rect, label, style);
	}
}
