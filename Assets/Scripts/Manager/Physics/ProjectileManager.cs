using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

	[Header("Object Throw Force")]
	public float throwForce = 40.0f;

	[Header("Object Prefab")]
	public GameObject prefabObj;

	private GameObject explosions;

	// Use this for initialization
	private void Start()
	{
		assignObjectVariables();
	}

	private void assignObjectVariables()
	{
		explosions = GameObject.Find("Explosions");
	}

	public void getMouseInputChange()
	{
		if (Input.GetMouseButtonDown(1))
		{
			calcProjectileAndThrow();
		}
	}

	private void calcProjectileAndThrow()
	{
		GameObject go = Instantiate(prefabObj, transform.position, transform.rotation);
		go.transform.parent = explosions.transform;

		Rigidbody rb = go.GetComponent<Rigidbody>();
		rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
	}
}
