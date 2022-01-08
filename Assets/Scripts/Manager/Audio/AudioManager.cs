using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	[Header("Gun SFX")]
	public AudioClip[] gunSfxList;

	[Header("Blood SFX")]
	public AudioClip[] bloodSfxList;

	[Header("Zombie Hit SFX")]
	public AudioClip[] zombieHitSfxList;

	[Header("Zombie Attack SFX")]
	public AudioClip[] zombieAttackSfxList;

	[Header("Player Hit SFX")]
	public AudioClip[] playerHitSfxList;

	private AudioSource gunAudioSource;
	private AudioSource bloodAudioSource;
	private AudioSource zombieAudioSource;
	private AudioSource playerAudioSource;

	private GameObject guns;
	private GameObject bloodSfx;
	private GameObject hordes;
	private GameObject player;

	// Use this for initialization
	private void Start()
	{
		assignObjectVariables();
		getAudioSource();
	}

	private void assignObjectVariables()
	{
		guns = GameObject.Find("Guns");
		bloodSfx = GameObject.Find("Blood SFX");
		hordes = GameObject.Find("Hordes");
		player = GameObject.Find("Player");
	}

	private void getAudioSource()
	{
		gunAudioSource = guns.GetComponent<AudioSource>();
		bloodAudioSource = bloodSfx.GetComponent<AudioSource>();
		zombieAudioSource = hordes.GetComponent<AudioSource>();
		playerAudioSource = player.GetComponent<AudioSource>();
	}

	public void playAudio(string type)
	{
		switch (type)
		{
			case "gun":
				switch (guns.GetComponent<GunManager>().getGunID())
				{
					case 0:
					case 8:
						startSinglePlayback(gunAudioSource, gunSfxList[guns.GetComponent<GunManager>().getGunID()]);
						break;

					default:
						startLoopPlayback(gunAudioSource, gunSfxList[guns.GetComponent<GunManager>().getGunID()]);
						break;
				}
				break;

			case "blood":
				startSinglePlayback(bloodAudioSource, bloodSfxList[Random.Range(0, bloodSfxList.Length)]);
				break;

			case "zombieHit":
				startSinglePlayback(zombieAudioSource, zombieHitSfxList[Random.Range(0, zombieHitSfxList.Length)]);
				break;

			case "zombieAttack":
				startLoopPlayback(zombieAudioSource, zombieAttackSfxList[Random.Range(0, zombieAttackSfxList.Length)]);
				break;

			case "playerHit":
				startLoopPlayback(playerAudioSource, playerHitSfxList[Random.Range(0, playerHitSfxList.Length)]);
				break;
		}
	}

	private void startSinglePlayback(AudioSource source, AudioClip clip)
	{
		source.clip = clip;
		source.Play();
		source.loop = false;
	}

	private void startLoopPlayback(AudioSource source, AudioClip clip)
	{
		if (!source.isPlaying)
		{
			source.clip = clip;
			source.Play();
			source.loop = false;
		}
	}
}
