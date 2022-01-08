using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{

	[Header("Object To Spawn")]
	public GameObject[] objectSpawnList;

	[Header("Object Y-Position")]
	public float[] objectPositionList;

	[HideInInspector]
	public Terrain terrain;
	[HideInInspector]
	public int posMin;
	[HideInInspector]
	public int posMax;
	[HideInInspector]
	public bool posMaxIsTerrainHeight;

	private int terrainWidth;
	private int terrainLength;
	private int terrainPosX;
	private int terrainPosZ;

	private GameObject ground;
	private GameObject player;

	// Use this for initialization
	private void Start()
	{
		assignObjectVariables();
		//initializeSpawnValues();
		StartCoroutine(spawnAtRandomLocation());
	}

	private void assignObjectVariables()
	{
		ground = GameObject.Find("Ground");
		player = GameObject.Find("Player");
	}

	private void initializeSpawnValues()
	{
		terrainWidth = (int) terrain.terrainData.size.x;
		terrainLength = (int) terrain.terrainData.size.z;
		terrainPosX = (int) terrain.transform.position.x;
		terrainPosZ = (int) terrain.transform.position.z;

		if(posMaxIsTerrainHeight == true)
		{
			posMax = (int) terrain.terrainData.size.y;
		}
	}
	
	public Vector3 getRandomPositionOnPlane(int randomValue)
	{
		Mesh planeMesh = ground.GetComponent<MeshFilter>().mesh;
		Bounds bounds = planeMesh.bounds;

		float minX = ground.transform.position.x - ground.transform.localScale.x * bounds.size.x * 0.5f;
		float minZ = ground.transform.position.z - ground.transform.localScale.z * bounds.size.z * 0.5f;

		Vector3 vector = new Vector3(Random.Range(minX, -minX), objectPositionList[randomValue], Random.Range(minZ, -minZ));

		return vector;
	}

	private Vector3 getRandomPositionOnTerrain(int randomValue)
	{
		int posx = Random.Range(terrainPosX, terrainPosX + terrainWidth);
		int posz = Random.Range(terrainPosZ, terrainPosZ + terrainLength);
		float posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, objectPositionList[randomValue], posz));

		if (posy < posMax && posy > posMin)
		{
			Vector3 vector = new Vector3(posx, posy, posz);

			return vector;
		}

		return Vector3.zero;
	}

	private IEnumerator spawnAtRandomLocation()
	{
		while (true)
		{
			if (player.GetComponent<StateManager>().getDead() == false)
			{
				int randomValue = Random.Range(0, objectSpawnList.Length);
				GameObject go = Instantiate(objectSpawnList[randomValue]) as GameObject;
				GameObject parent = gameObject.transform.parent.gameObject;
				go.transform.parent = parent.transform;

				go.transform.position = getRandomPositionOnPlane(randomValue);

				if (parent.tag == "Hordes")
				{
					go.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);

					yield return new WaitForSeconds(Random.Range(2.0f, 7.0f));
				}
				else
				{
					yield return new WaitForSeconds(Random.Range(15.0f, 21.0f));
				}
			}
			else
			{
				StopCoroutine(spawnAtRandomLocation());

				break;
			}
		}
	}
}
