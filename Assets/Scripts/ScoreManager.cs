using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public Transform player;
	public GameObject scoreUI;
	public GameObject scoreShadowUI;

	private int mScore = 0;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		mScore = (int)(player.position.z * 10);
		scoreUI.GetComponent<Text>().text = mScore.ToString();
		scoreShadowUI.GetComponent<Text>().text = mScore.ToString();
	}
}
