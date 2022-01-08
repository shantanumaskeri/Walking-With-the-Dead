using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleManager : MonoBehaviour
{

	[Header("Reticle Sprites")]
	public Sprite spriteAgentHeadLocked;
	public Sprite spriteAgentBodyLocked;
	public Sprite spriteAgentUnlocked;

	private bool agentLock = false;

	private GameObject agent;
	private GameObject guns;
	private GameObject bloodParticles;
	private GameObject bloodDecals;

	public GameObject Audio { get; set; }

	// Use this for initialization
	private void Start()
	{
		assignObjectVariables();
	}

	private void assignObjectVariables()
	{
		Audio = GameObject.Find("Audio");
		guns = GameObject.Find("Guns");
		bloodParticles = GameObject.Find("Blood Particles");
		bloodDecals = GameObject.Find("Blood Decals");
	}

	public void updateReticleAgentCollision()
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);

		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			switch (hit.collider.tag)
			{
				case "Zombie":
					GetComponent<SpriteRenderer>().sprite = spriteAgentBodyLocked;

					agentLock = true;
					agent = hit.collider.gameObject;
					break;

				case "ZombieHead":
					GetComponent<SpriteRenderer>().sprite = spriteAgentHeadLocked;

					agentLock = true;
					agent = hit.collider.gameObject;
					break;

				default:
					GetComponent<SpriteRenderer>().sprite = spriteAgentUnlocked;

					agentLock = false;
					agent = null;
					break;
			}
		}
	}

	public void getActiveGun()
	{
		switch (guns.GetComponent<GunManager>().getGunID())
		{
			case 0:
			case 8:
				if (Input.GetMouseButtonDown(0))
				{
					guns.GetComponent<GunManager>().fireActiveGun();
					checkIfAgentInFiringLine();
				}
				break;

			default:
				if (Input.GetMouseButton(0))
				{
					guns.GetComponent<GunManager>().fireActiveGun();
					checkIfAgentInFiringLine();
				}
				break;
		}

	}

	private void checkIfAgentInFiringLine()
	{
		if (agentLock)
		{
			if (agent != null)
			{
				switch (agent.tag)
				{
					case "Zombie":
						bloodParticles.GetComponent<ParticleManager>().showParticles(agent, new Vector3(0.0f, 1.0f, 0.3f), new Vector3(2.0f, 2.0f, 2.0f));
						bloodDecals.GetComponent<ParticleManager>().showParticles(bloodDecals, agent.transform.position, new Vector3(1.0f, 1.0f, 1.0f));

						Audio.GetComponent<AudioManager>().playAudio("blood");
						break;

					case "ZombieHead":
						GameObject go = agent.transform.parent.gameObject;
						go.GetComponent<AgentManager>().agentHit(go);
						break;
				}
			}
		}
	}

	public void disableReticle()
	{
		this.enabled = false;
		gameObject.SetActive(false);
	}
}
