using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour 
{

	[Header("Camera Follow Object")]
	public Transform playerTransform;

	private void LateUpdate()
	{
		Vector3 newPosition = playerTransform.position;
		newPosition.y = transform.position.y;
		transform.position = newPosition;

		transform.rotation = Quaternion.Euler(90.0f, playerTransform.eulerAngles.y, 0.0f);
	}
}
