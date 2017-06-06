/******************************************************************************
* 
* Class name: ScoreManager
* Created by: Edgard Damiani
* Description: Manages score according to player's movement and coin collection  
* 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	/*********************** Public properties *****************************/
	public Transform player;
	public GameObject scoreUI;
	public GameObject scoreShadowUI;


	/*********************** Private properties *****************************/
	private int mCoinScore = 0;
	private int mScore = 0;


	/*********************** Public methods *****************************/
	public void IncreaseCoinScore()
	{
		mCoinScore += 10;
	}


	/*********************** Private methods *****************************/
	private void Start ()
	{
		
	}

	private void Update ()
	{
		mScore = (int)(player.position.z * 10) + mCoinScore;
		scoreUI.GetComponent<Text>().text = mScore.ToString();
		scoreShadowUI.GetComponent<Text>().text = mScore.ToString();
	}

	
}
