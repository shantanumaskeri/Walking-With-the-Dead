using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

	public IEnumerator restartGame()
	{
		yield return new WaitForSeconds(7.0f);

		SceneManager.LoadScene("Main");
	}
}
