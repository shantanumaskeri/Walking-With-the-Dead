using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{

	[Header("Gun Prefab")]
	public GameObject[] gunList;

	[Header("Gun Recoil")]
	public float[] recoilList;

	[Header("Gun Key Code")]
	public KeyCode[] keyCodeList;

	[Header("Gun Max Hit")]
	public int[] hitCountList;

	[Header("Gun Availability")]
	public bool[] gunAvailabilityList;

	[Header("Gun Shells")]
	public GameObject[] gunShellList;

	[Header("Bullets Per Magazine")]
	public int[] bulletsPerMagList;

	[Header("Magazines Per Gun")]
	public int[] magsPerGunList;

	[Header("Shell Eject Position")]
	public Transform shellEjectPosition;

	[Header("Shell Eject Physics")]
	public float shellEjectForce;
	public float shellForceRandom;
	public float shellEjectTorqueX;
	public float shellEjectTorqueY;
	public float shellTorqueRandom;

	private AmmoManager ammoManager;
	private bool canReload = false;
	private float gunRecoilFactor = 0.0f;
	private float recoil = 0.0f;
	private float maxRecoil_x = -20f;
	private float maxRecoil_y = 20f;
	private float recoilSpeed = 2f;
	private GameObject muzzle;
	private GameObject ammunition;
	private int gunId = 0;

	public GameObject Audio { get; set; }

	// Use this for initialization
	private void Start()
	{
		//PlayerPrefs.DeleteAll();
		assignObjectVariables();
		getAmmunitionSource();
		setActiveGun(gunList[gunId], recoilList[gunId]);
		setActiveAmmunition();
	}

	private void assignObjectVariables()
	{
		Audio = GameObject.Find("Audio");
		muzzle = GameObject.Find("Muzzle");
		ammunition = GameObject.Find("Ammunition");
	}

	private void getAmmunitionSource()
	{
		ammoManager = GetComponentInChildren<AmmoManager>();
	}

	public void getKeyInputChange()
	{
		for (int i = 0; i < gunList.Length; i++)
		{
			if (Input.GetKeyDown(keyCodeList[i]))
			{
				if (gunAvailabilityList[i])
				{
					gunId = i;

					setActiveGun(gunList[i], recoilList[i]);
					setActiveAmmunition();
				}
			}
		}

		if (canReload)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				reloadGun();
			}
		}
	}

	private void setActiveGun(GameObject gun, float factor)
	{
		gunRecoilFactor = factor;

		Transform[] ta;
		ta = this.GetComponentsInChildren<Transform>();

		foreach (Transform t in ta)
		{
			if (t.gameObject.tag.Equals("Gun"))
			{
				t.gameObject.SetActive(false);
			}
		}

		gun.SetActive(true);
	}

	private void setActiveAmmunition()
	{
		ammoManager.totalBullets = bulletsPerMagList[gunId] - PlayerPrefs.GetInt("bullets" + gunId);
		ammoManager.totalMagazines = magsPerGunList[gunId] - PlayerPrefs.GetInt("mags" + gunId);
	}

	public void fireActiveGun()
	{
		ammoManager.totalBullets--;

		if (ammoManager.totalBullets < 0)
		{
			ammoManager.totalBullets = 0;
		}
		else
		{
			ammoManager.bulletsFired = bulletsPerMagList[gunId] - ammoManager.totalBullets;
			PlayerPrefs.SetInt("bullets" + gunId, ammoManager.bulletsFired);

			ejectBulletShells();
			setRecoilValues(0.2f, gunRecoilFactor, 10.0f);
			Audio.GetComponent<AudioManager>().playAudio("gun");

			canReload = true;
		}
	}

	public void showAmmunitionOnScreen()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, h - 40, w, h * 0.02f);
		style.alignment = TextAnchor.UpperLeft;
		style.font = (Font)Resources.Load("Fonts/consolaz");
		style.fontSize = h * 2 / 35;
		style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		string label = getGunName() + " AMMO \n" + ammoManager.totalBullets + " SHELLS/" + ammoManager.totalMagazines + " MAGS";
		GUI.Label(rect, label, style);
	}

	public int getGunID()
	{
		return gunId;
	}

	private string getGunName()
	{
		string gunName = gunList[gunId].name;
		return gunName.ToUpper();
	}

	private void setRecoilValues(float recoilParam, float maxRecoil_xParam, float recoilSpeedParam)
	{
		recoil = recoilParam;
		maxRecoil_x = maxRecoil_xParam;
		recoilSpeed = recoilSpeedParam;
		maxRecoil_y = Random.Range(-maxRecoil_xParam, maxRecoil_xParam);
	}

	public void applyRecoilToGun()
	{
		if (recoil > 0.0f)
		{
			Quaternion maxRecoil = Quaternion.Euler(maxRecoil_x, maxRecoil_y, 0.0f);

			transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed);
			recoil -= Time.deltaTime;

			muzzle.GetComponent<MuzzleManager>().setMuzzleFlash(true);
		}
		else
		{
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * recoilSpeed / 2);
			recoil = 0.0f;

			muzzle.GetComponent<MuzzleManager>().setMuzzleFlash(false);
		}
	}

	private void ejectBulletShells()
	{
		GameObject shell = Instantiate(gunShellList[gunId], shellEjectPosition.position, new Quaternion(Random.Range(0.0f, 180.0f), Random.Range(0.0f, 180.0f), Random.Range(0.0f, 180.0f), 0.0f)) as GameObject;
		shell.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(shellEjectForce + Random.Range(0, shellForceRandom), 0, 0), ForceMode.Impulse);
		shell.transform.parent = ammunition.transform;
	}

	private void reloadGun()
	{
		ammoManager.totalMagazines--;

		if (ammoManager.totalMagazines < 0)
		{
			ammoManager.totalMagazines = 0;
		}
		else
		{
			ammoManager.bulletsFired = 0;
			ammoManager.magazinesLoaded = magsPerGunList[gunId] - ammoManager.totalMagazines;

			PlayerPrefs.SetInt("mags" + gunId, ammoManager.magazinesLoaded);
			PlayerPrefs.SetInt("bullets" + gunId, ammoManager.bulletsFired);


			canReload = false;
			ammoManager.totalBullets = bulletsPerMagList[gunId];
		}
	}

	public void disableGuns()
	{
		this.enabled = false;
		gameObject.SetActive(false);
	}
}
