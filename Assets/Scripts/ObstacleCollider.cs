using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollider : MonoBehaviour
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
		//AudioSource audio = GetComponent<AudioSource>();
		//audio.Play();
		gameObject.transform.parent.gameObject.SetActive(false);

		other.gameObject.GetComponent<Player>().DecreaseEnergy();
		other.gameObject.GetComponent<AudioSource>().Play();
	}
}
