using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

	private void OnControllerColliderHit(ControllerColliderHit col)
	{
		if (col.gameObject.tag == "Ammo" || col.gameObject.tag == "Health")
		{
			Debug.Log(gameObject.name + " : " + col.gameObject.name);
		}
	}
}
