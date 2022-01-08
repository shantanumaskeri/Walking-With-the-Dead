using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour
{
	
	private bool isDead = false;
	private bool isAttacking = false;
	private bool isPositioned = false;

	private int hitCount = 0;

	[SerializeField]
	private float distanceToTarget;

	public void setAttack(bool pAttack)
	{
		isAttacking = pAttack;
	}

	public bool getAttack()
	{
		return isAttacking;
	}

	public void setDead(bool pDead)
	{
		isDead = pDead;
	}

	public bool getDead()
	{
		return isDead;
	}

	public void setHit(int pCount)
	{
		hitCount += pCount;
	}

	public int getHit()
	{
		return hitCount;
	}

	public void setPosition(bool pPosition)
	{
		isPositioned = pPosition;
	}

	public bool getPosition()
	{
		return isPositioned;
	}

	public void setDistance()
	{
		distanceToTarget = GetComponent<AgentManager>().agentDistance();
	}

	public float getDistance()
	{
		return distanceToTarget;
	}
}
