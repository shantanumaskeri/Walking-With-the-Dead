using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
	
	public GameObject flashlight;
	public GameObject lightObj;
	public float maxEnergy;
	
	private bool isLightEnabled = false;
	private float currentEnergy;
	private float usedEnergy;
	private int batteries;
	private GameObject batteryPickedUp;
	
	// Use this for initialization
	private void Start()
	{
		
	}
	
	// Update is called once per frame
	private void FixedUpdate()
	{
		maxEnergy = 50 * batteries;
		currentEnergy = maxEnergy;
		
		if (Input.GetKeyDown(KeyCode.F))
		{
			isLightEnabled = !isLightEnabled;
		}
		
		flashlight.SetActive(isLightEnabled);
		
		if (isLightEnabled)
		{
			if (currentEnergy <= 0.0f)
			{
				lightObj.SetActive(false);
				batteries = 0;
			}
			else
			{
				lightObj.SetActive(true);
				currentEnergy -= 0.5f * Time.deltaTime;
				usedEnergy += 0.5f * Time.deltaTime;
				
				if (usedEnergy >= 50.0f)
				{
					batteries -= 1;
					usedEnergy = 0.0f;
				}
			}
		}
	}
	
	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Battery")
		{
			batteryPickedUp = col.gameObject;
			batteries += 1;
			
			Destroy(batteryPickedUp);
		}
	}
}