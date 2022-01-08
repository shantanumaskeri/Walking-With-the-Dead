using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour 
{

	[Header("Explosion Delay")]
	public float delay = 3.0f;

	[Header("Explosion Radius")]
	public float radius = 5.0f;

	[Header("Explosion Force")]
	public float force = 700.0f;

	[Header("Explosion Effect")]
	public GameObject effect;

	[Header("Nav Agent Dead Prefab")]
	public GameObject ragdollWithHead;

	private bool hasExploded = false;

	private float countdown;

	private GameObject explosions;

	// Use this for initialization
	private void Start() 
	{
		assignObjectVariables();
	}

	private void assignObjectVariables()
	{
		explosions = GameObject.Find("Explosions");
		countdown = delay;
	}

	// Update is called once per frame
	private void Update() 
	{
		startTimer();
	}

	public void startTimer()
	{
		countdown -= Time.deltaTime;

		if (countdown <= 0.0f && !hasExploded)
		{
			triggerExplosion();

			hasExploded = true;
		}
	}

	private void triggerExplosion()
	{
		GameObject explosionEffect = Instantiate(effect, transform.position, transform.rotation) as GameObject;
		explosionEffect.transform.parent = explosions.transform;

		Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

		foreach (Collider nearbyObject in collidersToDestroy)
		{
			if (nearbyObject.gameObject.tag == "Zombie")
			{
				nearbyObject.gameObject.GetComponent<AgentManager>().agentDead(nearbyObject.gameObject, ragdollWithHead);
			}
			else if (nearbyObject.gameObject.tag == "Player")
			{
				nearbyObject.gameObject.GetComponent<PlayerManager>().playerHit(2);
			}
		}

		Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

		foreach (Collider nearbyObject in collidersToMove)
		{
			Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.AddExplosionForce(force, transform.position, radius);
			}
		}

		Destroy(gameObject);
	}
}
