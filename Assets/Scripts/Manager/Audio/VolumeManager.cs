using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour 
{

	private AudioSource audioSource;
	private float distanceToTarget;
	//private GameObject player;
	private GameObject[] hordes;

	// Use this for initialization
	private void Start () 
	{
		//assignObjectVariables();
		getAudioSource();
	}

	// Update is called once per frame
	private void Update()
	{
		//adjustVolume();
	}

	/*private void assignObjectVariables()
	{
		player = GameObject.Find("Player");
	}*/

	private void getAudioSource()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void adjustVolume()
	{
		if (hordes != null)
		{
			hordes = GameObject.FindGameObjectsWithTag("Zombie");

			foreach (GameObject go in hordes)
			{
				distanceToTarget = go.GetComponent<AgentManager>().agentDistance();
			}

			audioSource.volume = 10 / distanceToTarget;
		}
	}
}
