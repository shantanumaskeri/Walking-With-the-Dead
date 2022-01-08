using UnityEngine;
using System.Collections;

public class DamageManager : MonoBehaviour 
{
	
	[Header("Player Damage")]
	public GameObject[] damageIndictors;

	private bool isActive = false;
	private bool isFading = false;

	private float alpha = 0.0f;

	private GameObject player;

	// Use this for initialization
	private void Start()
	{
		assignObjectVariables();
	}

	private void assignObjectVariables()
	{
		player = GameObject.Find("Player");
	}

	public void animateDamageAlpha()
	{
		if (getFading() == true)
		{
			if (player.GetComponent<StateManager>().getDead() == true)
			{
				return;
			}
			else
			{
				if (alpha > 0.0f)
				{
					alpha -= 0.1f;

					assignDamageAlpha();
				}
				else
				{
					alpha = 0.0f;

					disableFading();
					disableActivity();
					assignDamageAlpha();

					for (int i = 0; i < damageIndictors.Length; i++)
					{
						damageIndictors[i].SetActive(false);
					}
				}
			}
		}
		else
		{
			if (isActive)
			{
				if (alpha < 1.0f)
				{
					alpha = 1.0f;

					for (int i = 0; i < damageIndictors.Length; i++)
					{
						damageIndictors[i].SetActive(true);
					}
					
					enableFading();
					assignDamageAlpha();
				}
			}
		}
	}

	private void assignDamageAlpha()
	{
		for (int i = 0; i < damageIndictors.Length; i++)
		{
			damageIndictors[i].GetComponent<SpriteRenderer>().material.color = new Color(damageIndictors[i].GetComponent<SpriteRenderer>().material.color.r, damageIndictors[i].GetComponent<SpriteRenderer>().material.color.g, damageIndictors[i].GetComponent<SpriteRenderer>().material.color.b, alpha);
		}
	}

	public void enableActivity()
	{
		isActive = true;
	}

	public void disableActivity()
	{
		isActive = false;
	}

	public void enableFading()
	{
		isFading = true;
	}

	public void disableFading()
	{
		isFading = false;
	}

	public bool getFading()
	{
		return isFading;
	}
}
