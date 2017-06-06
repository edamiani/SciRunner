using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuiManager : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		transform.Find("GameOver").gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

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
}
