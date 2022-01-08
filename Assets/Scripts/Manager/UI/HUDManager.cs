using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{

	private int agentsKilled = 0;
	
	public void incrementStatistics()
	{
		agentsKilled += 1;
		if (agentsKilled % 5 == 0)
		{
			// powerups will be dropped here and remains for 10 seconds on screen. collect to unlock weapon
			//GameObject.Find("Weapons").GetComponent<WeaponManager>().weaponAvailabilityList[agentsKilled / 5] = true;
		}
	}
}
