using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerManager : MonoBehaviour 
{

	public GameObject Audio { get; set; }

	private GameObject guns;
	private GameObject reticle;
	private GameObject muzzle;
	private GameObject damage;
	private GameObject game;

	// Use this for initialization
	private void Start()
	{
		assignObjectVariables();
	}

	private void assignObjectVariables()
	{
		guns = GameObject.Find("Guns");
		reticle = GameObject.Find("Reticle");
		muzzle = GameObject.Find("Muzzle");
		damage = GameObject.Find("Damage");
		Audio = GameObject.Find("Audio");
		game = GameObject.Find("Game");
	}

	public void playerHit(int hitValue)
	{
		damage.GetComponent<DamageManager>().disableFading();
		damage.GetComponent<DamageManager>().enableActivity();

		GetComponent<StateManager>().setHit(hitValue);

		Audio.GetComponent<AudioManager>().playAudio("playerHit");

		if (GetComponent<StateManager>().getHit() >= 10)
		{
			GetComponent<StateManager>().setDead(true);

			GetComponent<PlayerManager>().playerDead();
		}
	}

	public void playerDead()
	{
		guns.GetComponent<GunManager>().disableGuns();
		reticle.GetComponent<ReticleManager>().disableReticle();
		muzzle.GetComponent<MuzzleManager>().disableMuzzleFlash();

		disableMovement();
		animateUsingPhysics();

		StartCoroutine(game.GetComponent<GameManager>().restartGame());
	}

	private void disableMovement()
	{
		GetComponent<CharacterController>().enabled = false;
		GetComponent<FirstPersonController>().enabled = false;
		GetComponent<CapsuleCollider>().enabled = true;
	}

	private void animateUsingPhysics()
	{
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * 200);
	}
}
