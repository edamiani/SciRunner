/******************************************************************************
* 
* Class name: CoinManager
* Created by: Edgard Damiani
* Description: Manages the creation and destruction of coins
* 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
	/*********************** Public properties *****************************/
	/// <summary>Object's Transform that will be used to track coins' 
	/// life span.</summary>
	public Transform	targetToTrack;

	/// <summary>The object pooler that will hold the coins.</summary>
	public ObjectPooler	coinPooler;

	/// <summary>Distance between coins.</summary>
	public float		distanceBetweenCoins = 3;

	/// <summary>Number of coins to be created ahead of the 
	/// tracked object.</summary>
	public int			coinsAfterTrackedObject = 30;


	/*********************** Private properties *****************************/
	/// <summary>A doubly-linked list of coins.</summary>
	private LinkedList<GameObject>
						mCoinList = new LinkedList<GameObject>();

	/// <summary>Holds the distance that should be traversed by the
	/// tracked object so that new coins are creatd.</summary>
	private float		mDistanceAfter;	


	/*********************** Private methods *****************************/
	void Start()
	{
		Vector3 position = targetToTrack.position;

		mDistanceAfter = position.z + coinsAfterTrackedObject * distanceBetweenCoins;

		position.z += distanceBetweenCoins;

		for(int i = 0; i < coinsAfterTrackedObject; i++)
		{
			Vector3 tempPosition = position;

			GameObject coin = coinPooler.GetObject();

			// Defines a coin's position on the X-axis inside the [-1, 2) range
			// This could be modified to generalize the range and distance between objects
			int xOffset = Random.Range(-1, 2);
			Vector3 innerOffset = Vector3.zero;

			if(xOffset == -1)
			{
				innerOffset.x -= 1;
			}
			else if(xOffset == 1)
			{
				innerOffset.x += 1;
			}

			coin.transform.position = tempPosition;

			Transform innerTransform = coin.transform.Find("Inner").transform;
			innerTransform.Translate(innerOffset);

			// Activates the coin, add it to the coin list and position it on the Z-axis
			// (Coins will always be inserted at the end of the list and removed from
			// the beginning)
			coin.SetActive(true);			
			mCoinList.AddLast(coin);
			position.z += distanceBetweenCoins;
		}
	}
	
	void Update()
	{
		Vector3 targetPosition = targetToTrack.position;

		// Is the tracked object getting closer to the prescribed distance so new coins should be added?
		if(targetPosition.z > mDistanceAfter - ((coinsAfterTrackedObject / 3) * distanceBetweenCoins))
		{
			// Removes the list's first coin from scene so it can be re-added as a new coin
			mCoinList.First.Value.SetActive(false);
			mCoinList.RemoveFirst();

			GameObject coin = coinPooler.GetObject();
			Transform innerTransform = coin.transform.Find("Inner").transform;

			// Resets obstacle's local position
			Vector3 innerPosition = innerTransform.position;
			innerPosition.x = 0;
			innerTransform.position = innerPosition;

			// Get the frontmost tile's position and adjust it
			Vector3 coinPosition = mCoinList.Last.Value.transform.position;
			coinPosition.z = mDistanceAfter;

			// Position the new coin on the X-Axis
			int xOffset = Random.Range(-1, 2);
			Vector3 innerOffset = Vector3.zero;

			if(xOffset == -1)
			{
				innerOffset.x -= 1;
			}
			else if(xOffset == 1)
			{
				innerOffset.x += 1;
			}

			coin.transform.position = coinPosition;

			innerTransform.Translate(innerOffset);

			// Add the new coin to the scene and update mDistanceAfter
			coin.SetActive(true);
			mCoinList.AddLast(coin);
			mDistanceAfter += distanceBetweenCoins;

			// Needed because the coin's renderer disabled when a collision happens
			coin.GetComponentInChildren<SpriteRenderer>().enabled = true;
		}

	}
}
