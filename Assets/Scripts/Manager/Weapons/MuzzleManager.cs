using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleManager : MonoBehaviour
{
	
	[Header("Gun Muzzle Flash")]
	public GameObject[] gunParticleList;

	private GameObject guns;

	// Use this for initialization
	private void Start()
	{
		assignObjectVariables();
	}

	private void assignObjectVariables()
	{
		guns = GameObject.Find("Guns");
	}

	public void setMuzzleFlash(bool render)
	{
		if (gunParticleList[guns.GetComponent<GunManager>().getGunID()] != null)
		{
			gunParticleList[guns.GetComponent<GunManager>().getGunID()].SetActive(render);
		}
	}

	public void disableMuzzleFlash()
	{
		this.enabled = false;
		gameObject.SetActive(false);
	}
}
