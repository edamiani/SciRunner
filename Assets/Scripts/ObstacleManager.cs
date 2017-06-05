using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
	public float		distanceToRemove = 50;
	public Transform	targetToTrack;
	public ObjectPooler	obstaclePooler;
	public float		distanceBetweenObstacles = 1;
	public int			obstaclesAfterPlayer = 5;
	public int			obstaclesBeforePlayer = 2;

	private float		mDistanceAfter;
	private float		mDistanceBefore;
	private LinkedList<GameObject> 
						mObstacleList = new LinkedList<GameObject>();

	// Use this for initialization
	void Start ()
	{
		obstaclePooler.Initialize();

		Vector3 position = targetToTrack.position;
		position.y += 0.5f;

		mDistanceBefore = position.z - obstaclesBeforePlayer * distanceBetweenObstacles;
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
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 targetPosition = targetToTrack.position;

		if(targetPosition.z > mDistanceAfter - (3 * distanceBetweenObstacles))
		{
			mObstacleList.First.Value.SetActive(false);
			mObstacleList.RemoveFirst();
			mDistanceBefore += distanceBetweenObstacles;

			GameObject obstacle = obstaclePooler.GetObject();
			Transform innerTransform = obstacle.transform.Find("Inner").transform;

			// Resets obstacle's local position
			Vector3 innerPosition = innerTransform.position;
			innerPosition.x = 0;
			innerTransform.position = innerPosition;

			// Get the frontmost tile's position and adjust it
			Vector3 obstaclePosition = mObstacleList.Last.Value.transform.position;
			obstaclePosition.z = mDistanceAfter;
			//tilePosition.y += 0.5f;

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
