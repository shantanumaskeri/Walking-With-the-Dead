using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class AgentManager : MonoBehaviour 
{

	[Header("Ragdoll Headless")]
	public GameObject ragdollHeadless;

	public Animation Animation { get; set; }

	public GameObject Audio { get; set; }

	private NavMeshAgent agent;

	private GameObject player;
	private GameObject damage;
	private GameObject guns;
	private GameObject bloodParticles;
	private GameObject bloodDecals;
	private GameObject hordes;
	private GameObject hud;
	private GameObject reticle;
	private GameObject zombieSpawner;
	private GameObject muzzle;
	
	private Vector3 endPosition;

	// Use this for initialization
	private void Start() 
	{
		assignObjectVariables();
		initializeAgent();
	}

	// Update is called once per frame
	private void Update() 
	{
		setupAgentLocomotion();
	}

	private void assignObjectVariables()
	{
		player = GameObject.Find("Player");
		Audio = GameObject.Find("Audio");
		damage = GameObject.Find("Damage");
		guns = GameObject.Find("Guns");
		bloodParticles = GameObject.Find("Blood Particles");
		bloodDecals = GameObject.Find("Blood Decals");
		hordes = GameObject.Find("Hordes");
		hud = GameObject.Find("HUD");
		zombieSpawner = GameObject.Find("Zombie Spawn");
	}

	private void initializeAgent()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.speed = Random.Range(3, 6);

		Animation = GetComponent<Animation>();
		Animation["walk"].speed = agent.speed * 0.25f;
	}

	private void setupAgentLocomotion()
	{
		if (GetComponent<StateManager>().getDead() == false)
		{
			if (player.GetComponent<StateManager>().getDead() == false)
			{
				if (agentDone())
				{
					if (agentNotZero())
					{
						Animation.CrossFade("attack");

						GetComponent<StateManager>().setAttack(true);

						Audio.GetComponent<AudioManager>().playAudio("zombieAttack");
					}
				}
				else
				{
					Animation.CrossFade("walk");

					GetComponent<StateManager>().setAttack(false);

					if (damage.GetComponent<DamageManager>().getFading() == false)
					{
						damage.GetComponent<DamageManager>().enableFading();
					}
				}

				agent.SetDestination(player.transform.position);
			}
			else
			{
				Animation.CrossFade("walk");

				if (GetComponent<StateManager>().getPosition() == false)
				{
					endPosition = zombieSpawner.GetComponent<SpawnManager>().getRandomPositionOnPlane(0);

					GetComponent<StateManager>().setPosition(true);
				}
				
				agent.SetDestination(endPosition);
			}
		}
		else
		{
			agent.isStopped = true;
			agent.ResetPath();
		}
	}

	private void setupAgentAttack()
	{
		if (GetComponent<StateManager>().getDead() == false)
		{
			if (player.GetComponent<StateManager>().getDead() == false)
			{
				if (agentDone())
				{
					if (agentNotZero())
					{
						player.GetComponent<PlayerManager>().playerHit(1);
					}
				}
			}
		}
	}

	private bool agentDone()
	{
		return agent.remainingDistance <= agent.stoppingDistance;
	}

	private bool agentNotZero()
	{
		return agent.remainingDistance != 0;
	}

	public float agentDistance()
	{
		return (agent.remainingDistance - agent.stoppingDistance);
	}

	public void agentHit(GameObject go)
	{
		Audio.GetComponent<AudioManager>().playAudio("blood");
		Audio.GetComponent<AudioManager>().playAudio("zombieHit");

		go.GetComponent<StateManager>().setHit(1);

		if (go.GetComponent<StateManager>().getHit() >= guns.GetComponent<GunManager>().hitCountList[guns.GetComponent<GunManager>().getGunID()])
		{
			agentDead(go, ragdollHeadless);
			//go.GetComponent<MeshDestroyer>().splitMesh(true);
		}
		else
		{
			bloodParticles.GetComponent<ParticleManager>().showParticles(go, new Vector3(0.0f, 1.5f, 0.6f), new Vector3(1.0f, 1.0f, 1.0f));
			bloodDecals.GetComponent<ParticleManager>().showParticles(bloodDecals, go.transform.position, new Vector3(1.0f, 1.0f, 1.0f));
		}
	}

	public void agentDead(GameObject go, GameObject ragdoll)
	{
		GameObject rd =  Instantiate (ragdoll, go.transform.position, Quaternion.Euler (0.0f, 180.0f, 0.0f)) as GameObject;
		rd.transform.parent = hordes.transform;

		bloodParticles.GetComponent<ParticleManager>().showParticles(rd, new Vector3(0.0f, 1.5f, 0.3f), new Vector3(1.0f, 1.0f, 1.0f));
		bloodDecals.GetComponent<ParticleManager>().showParticles(bloodDecals, rd.transform.position, new Vector3(1.0f, 1.0f, 1.0f));

		go.GetComponent<StateManager>().setAttack(false);
		go.GetComponent<StateManager>().setDead(true);
		Destroy(go);

		hud.GetComponent<HUDManager>().incrementStatistics();
		damage.GetComponent<DamageManager>().disableActivity();
	}
}
