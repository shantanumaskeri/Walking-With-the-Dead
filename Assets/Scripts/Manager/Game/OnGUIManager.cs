using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGUIManager : MonoBehaviour 
{

	private GameObject sun;
	private GameObject fps;
	private GameObject guns;

	private SunlightManager sunlightManager;
	private FPSManager fpsManager;
	private GunManager gunManager; 

	// Use this for initialization
	private void Start()
	{
		assignObjectVariables();
		getScriptComponents();
	}

	// OnGUI is called every frame to draw GUI on screen
	private void OnGUI()
	{
		sunlightManager.showDateAndTimeOnScreen();
		fpsManager.showFpsOnScreen();
		gunManager.showAmmunitionOnScreen();
	}

	private void assignObjectVariables()
	{
		sun = GameObject.Find("Sun");
		fps = GameObject.Find("FPS");
		guns = GameObject.Find("Guns");
	}

	private void getScriptComponents()
	{
		sunlightManager = sun.GetComponent<SunlightManager>();
		fpsManager = fps.GetComponent<FPSManager>();
		gunManager = guns.GetComponent<GunManager>();
	}
}
