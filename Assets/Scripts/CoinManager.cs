using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
	public float		distanceToRemove = 50;
	public Transform	targetToTrack;
	public ObjectPooler	coinPooler;
	public float		distanceBetweenCoins = 3;
	public int			coinsAfterPlayer = 30;

	private float		mDistanceAfter;
	private LinkedList<GameObject> 
						mCoinList = new LinkedList<GameObject>();

	// Use this for initialization
	void Start ()
	{
		coinPooler.Initialize();

		Vector3 position = targetToTrack.position;

		mDistanceAfter = position.z + coinsAfterPlayer * distanceBetweenCoins;

		position.z += distanceBetweenCoins;

		for(int i = 0; i < coinsAfterPlayer; i++)
		{
			Vector3 tempPosition = position;

			GameObject coin = coinPooler.GetObject();

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

			coin.SetActive(true);

			mCoinList.AddLast(coin);

			position.z += distanceBetweenCoins;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 targetPosition = targetToTrack.position;

		if(targetPosition.z > mDistanceAfter - (3 * distanceBetweenCoins))
		{
			mCoinList.First.Value.SetActive(false);
			mCoinList.RemoveFirst();

			GameObject obstacle = coinPooler.GetObject();
			Transform innerTransform = obstacle.transform.Find("Inner").transform;

			// Resets obstacle's local position
			Vector3 innerPosition = innerTransform.position;
			innerPosition.x = 0;
			innerTransform.position = innerPosition;

			// Get the frontmost tile's position and adjust it
			Vector3 obstaclePosition = mCoinList.Last.Value.transform.position;
			obstaclePosition.z = mDistanceAfter;

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

			obstacle.transform.position = obstaclePosition;

			innerTransform.Translate(innerOffset);

			obstacle.SetActive(true);

			mCoinList.AddLast(obstacle);

			mDistanceAfter += distanceBetweenCoins;
		}

	}
}
