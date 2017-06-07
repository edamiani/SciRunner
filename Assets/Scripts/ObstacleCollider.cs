/******************************************************************************
* 
* Class name: ObstacleCollider
* Created by: Edgard Damiani
* Description: Takes care of an obstacle's collision 
* 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollider : MonoBehaviour
{
	/*********************** Private methods *****************************/
	private void Start()
	{

	}
	
	private void Update()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		gameObject.transform.parent.gameObject.SetActive(false);

		other.gameObject.GetComponent<Player>().DecreaseEnergy();
		other.gameObject.GetComponent<AudioSource>().Play();
	}
}
