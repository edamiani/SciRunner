/******************************************************************************
* 
* Class name: ObstacleManager
* Created by: Edgard Damiani
* Description: Manages the creation and destruction of obstacles
* 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
	/*********************** Public properties *****************************/
	/// <summary>Object's Transform that will be used to track obstacles' 
	/// life span.</summary>
	public Transform	targetToTrack;

	/// <summary>The object pooler that will hold the obstacles.</summary>
	public ObjectPooler	obstaclePooler;

	/// <summary>Distance between obstacles.</summary>
	public float		distanceBetweenObstacles = 1;

	/// <summary>Number of obstacles to be kept behind the 
	/// tracked object.</summary>
	public int obstaclesBeforePlayer = 2;

	/// <summary>Number of obstacles to be created ahead of the 
	/// tracked object.</summary>
	public int			obstaclesAfterPlayer = 5;


	/*********************** Private properties *****************************/
	/// <summary>Holds the distance that should be traversed by the
	/// tracked object so that new obstacles are creatd.</summary>
	private float		mDistanceAfter;

	/// <summary>Holds the distance that should be traversed by the
	/// tracked object so that old obstacles are removed.</summary>
	//private float		mDistanceBefore;

	/// <summary>A doubly-linked list of obstacles.</summary>
	private LinkedList<GameObject> 
						mObstacleList = new LinkedList<GameObject>();


	/*********************** Private methods *****************************/
	private void Start ()
	{
		Vector3 position = targetToTrack.position;
		position.y += 0.5f;

		//mDistanceBefore = position.z - obstaclesBeforePlayer * distanceBetweenObstacles;
		mDistanceAfter = position.z + obstaclesAfterPlayer * distanceBetweenObstacles;

		position.z += distanceBetweenObstacles;

		for(int i = 0; i < obstaclesBeforePlayer + obstaclesAfterPlayer; i++)
		{
			Vector3 tempPosition = position;

			GameObject obstacle = obstaclePooler.GetObject();

			Renderer tileRenderer = obstacle.GetComponentInChildren<Renderer>();
			Color color = tileRenderer.material.color;
			color.r = Random.Range(0.0f, 1.0f);
			color.g = Random.Range(0.0f, 1.0f);
			color.b = Random.Range(0.0f, 1.0f);
			obstacle.GetComponentInChildren<Renderer>().material.color = color;

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

			obstacle.transform.position = tempPosition;

			Transform innerTransform = obstacle.transform.Find("Inner").transform;
			innerTransform.Translate(innerOffset);

			obstacle.SetActive(true);

			mObstacleList.AddLast(obstacle);

			position.z += distanceBetweenObstacles;
		}
	}
	
	private void Update()
	{
		Vector3 targetPosition = targetToTrack.position;

		if(targetPosition.z > mDistanceAfter - (3 * distanceBetweenObstacles))
		{
			mObstacleList.First.Value.SetActive(false);
			mObstacleList.RemoveFirst();
			//mDistanceBefore += distanceBetweenObstacles;

			GameObject obstacle = obstaclePooler.GetObject();
			Transform innerTransform = obstacle.transform.Find("Inner").transform;

			// Resets obstacle's local position
			Vector3 innerPosition = innerTransform.position;
			innerPosition.x = 0;
			innerTransform.position = innerPosition;

			// Get the frontmost tile's position and adjust it
			Vector3 obstaclePosition = mObstacleList.Last.Value.transform.position;
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

			Renderer tileRenderer = obstacle.GetComponentInChildren<Renderer>();
			Color color = tileRenderer.material.color;
			color.r = Random.Range(0.0f, 1.0f);
			color.g = Random.Range(0.0f, 1.0f);
			color.b = Random.Range(0.0f, 1.0f);
			tileRenderer.material.color = color;

			mObstacleList.AddLast(obstacle);

			mDistanceAfter += distanceBetweenObstacles;
		}

	}
}
