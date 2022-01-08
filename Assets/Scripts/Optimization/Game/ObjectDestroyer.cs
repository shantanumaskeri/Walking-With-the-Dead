using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{

	[Header("Object Destroy Time")]
	public float destroyTime = 5.0f;

	// Use this for initialization
	private void Start()
	{
		destroyObject();
	}

	private void destroyObject()
	{
		Destroy(gameObject, destroyTime);
}
}
