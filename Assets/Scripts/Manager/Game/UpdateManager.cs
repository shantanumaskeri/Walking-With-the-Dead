using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour 
{

	private GameObject reticle;
	private GameObject guns;
	private GameObject damage;
	private GameObject sun;
	private GameObject fps;
	private GameObject agent;

	private ReticleManager reticleManager;
	private GunManager gunManager;
	private DamageManager damageManager;
	private ProjectileManager projectileManager;
	private SunlightManager sunlightManager;
	private FPSManager fpsManager;
	private AgentManager agentManager;

	public GameObject Camera { get; set; }

	// Use this for initialization
	private void Start()
	{
		assignObjectVariables();
		getScriptComponents();
	}

	// Update is called once per frame
	private void Update() 
	{
		reticleManager.updateReticleAgentCollision();
		reticleManager.getActiveGun();

		gunManager.getKeyInputChange();
		gunManager.applyRecoilToGun();

		damageManager.animateDamageAlpha();

		projectileManager.getMouseInputChange();

		sunlightManager.getTime();
		sunlightManager.getDate();
		sunlightManager.changeLightIntensity();
		sunlightManager.animateFogColor();

		fpsManager.incrementTime();
	}

	private void assignObjectVariables()
	{
		reticle = GameObject.Find("Reticle");
		guns = GameObject.Find("Guns");
		damage = GameObject.Find("Damage");
		Camera = GameObject.FindGameObjectWithTag("MainCamera");
		sun = GameObject.Find("Sun");
		fps = GameObject.Find("FPS");
	}

	private void getScriptComponents()
	{
		reticleManager = reticle.GetComponent<ReticleManager>();
		gunManager = guns.GetComponent<GunManager>();
		damageManager = damage.GetComponent<DamageManager>();
		projectileManager = Camera.GetComponent<ProjectileManager>();
		sunlightManager = sun.GetComponent<SunlightManager>();
		fpsManager = fps.GetComponent<FPSManager>();
	}
}
