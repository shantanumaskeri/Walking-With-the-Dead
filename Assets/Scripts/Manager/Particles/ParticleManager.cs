using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

	[Header("Blood Particles")]
	public GameObject[] bloodParticleList;
	
	public void showParticles(GameObject parent, Vector3 pos, Vector3 scale)
	{
		GameObject particle = Instantiate(bloodParticleList[Random.Range(0, bloodParticleList.Length)]) as GameObject;
		particle.transform.parent = parent.transform;
		particle.transform.localPosition = pos;
		particle.transform.localRotation = Quaternion.identity;
		particle.transform.localScale = scale;

		if (gameObject.name == "Blood Particles")
		{
			if (parent.tag != "Ragdoll")
			{
				var reactivator = particle.AddComponent<DemoReactivator>();
				reactivator.TimeDelayToReactivate = 1.5f;
			}
		}
	}
}
