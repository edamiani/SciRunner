/******************************************************************************
* 
* Class name: MainMenuManager
* Created by: Edgard Damiani
* Description: Manages the Main Menu's GUI interaction 
* 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	/*********************** Public methods *****************************/
	public void LoadGameScene()
	{
		SceneManager.LoadScene("Game");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	/*********************** Private methods *****************************/
	void Start()
	{
		
	}
	
	void Update()
	{
		
	}
}
