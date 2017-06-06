using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollider : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponentInChildren<CharacterController>() != null)
		{
			//AudioSource audio = GetComponent<AudioSource>();
			//audio.Play();
			gameObject.transform.parent.gameObject.SetActive(false);

			GameObject.Find("ScoreManager").GetComponent<ScoreManager>().IncreaseCoinScore();

			//other.gameObject.GetComponent<Player>().DecreaseEnergy();
			//other.gameObject.GetComponent<AudioSource>().Play();
		}
	}
}
