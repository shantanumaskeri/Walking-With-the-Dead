using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaManager : MonoBehaviour
{

	private Color startColor;
	private Color endColor;

	private float delay;
	
	// Use this for initialization
	private void Start()
	{
		setInitialColor();
	}

	// Update is called once per frame
	private void Update()
	{
		//changeAlphaOnColor();
	}

	private void setInitialColor()
	{
		startColor = GetComponent<MeshRenderer>().material.color;
		endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
	}

	private void changeAlphaOnColor()
	{
		delay += Time.deltaTime;

		GetComponent<MeshRenderer>().material.color = Color.Lerp(startColor, endColor, delay * 0.1f);

		if (GetComponent<MeshRenderer>().material.color.a == 0.0f)
		{
			Destroy(gameObject);
		}
	}
}
