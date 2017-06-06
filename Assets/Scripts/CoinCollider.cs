/******************************************************************************
* 
* Class name: CoinCollider
* Created by: Edgard Damiani
* Description: Takes care of a coin's collision 
* 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollider : MonoBehaviour
{
	/*********************** Private methods *****************************/
	void Start()
	{

	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponentInChildren<CharacterController>() != null)
		{
			GameObject.Find("ScoreManager").GetComponent<ScoreManager>().IncreaseCoinScore();

			AudioSource audio = gameObject.GetComponent<AudioSource>();
			//audio.Play();
			StartCoroutine(PlaySound(audio));
		}

		//gameObject.transform.parent.gameObject.SetActive(false);
	}

	IEnumerator PlaySound(AudioSource audio)
	{
		gameObject.GetComponent<SpriteRenderer>().enabled = false;

		audio.Play();
		yield return new WaitForSeconds(audio.clip.length);

		//gameObject.SetActive(false);
		gameObject.transform.parent.gameObject.SetActive(false);
	}
}
