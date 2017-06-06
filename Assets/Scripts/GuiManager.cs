/******************************************************************************
* 
* Class name: GuiCollider
* Created by: Edgard Damiani
* Description: Manages the in-game's GUI interaction 
* 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuiManager : MonoBehaviour
{
	/*********************** Public methods *****************************/
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void ResetScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ShowGameOverMenu()
	{
		transform.Find("GameOver").gameObject.SetActive(true);
	}

	/*********************** Public methods *****************************/
	void Start()
	{
		transform.Find("GameOver").gameObject.SetActive(false);
	}
	
	void Update()
	{
		
	}
}
