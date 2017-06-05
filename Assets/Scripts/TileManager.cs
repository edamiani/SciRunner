using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
	public float		distanceToRemove = 50;
	public Transform	targetToTrack;
	public ObjectPooler	tilePooler;
	public float		tileSize = 1;
	public int			tilesAfterPlayer = 5;
	public int			tilesBeforePlayer = 2;

	private float		mDistanceAfter;
	private float		mDistanceBefore;
	private LinkedList<GameObject> 
						mTileList = new LinkedList<GameObject>();

	// Use this for initialization
	void Start ()
	{
		tilePooler.Initialize();

		Vector3 position = targetToTrack.position;
		position.y -= 1;

		mDistanceBefore = position.z - tilesBeforePlayer * tileSize;
		mDistanceAfter = position.z + tilesAfterPlayer * tileSize;

		position.z -= tilesBeforePlayer * tileSize;

		for(int i = 0; i < tilesBeforePlayer + tilesAfterPlayer; i++)
		{
			GameObject tile = tilePooler.GetObject();

			Renderer tileRenderer = tile.GetComponentInChildren<Renderer>();
			Color color = tileRenderer.material.color;
			color.r = Random.Range(0.0f, 1.0f);
			color.g = Random.Range(0.0f, 1.0f);
			color.b = Random.Range(0.0f, 1.0f);
			tile.GetComponentInChildren<Renderer>().material.color = color;

			tile.transform.position = position;

			Vector3 scale = Vector3.zero;
			scale.x = scale.y = scale.z = 0.999f;
			tile.transform.localScale = scale;

			tile.SetActive(true);

			mTileList.AddLast(tile);

			position.z += tileSize;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 targetPosition = targetToTrack.position;

		//if(targetPosition.z > mDistanceBehind - tileSize)
		//{
			
		//}

		if(targetPosition.z > mDistanceAfter - (3 * tileSize))
		{
			mTileList.First.Value.SetActive(false);
			mTileList.RemoveFirst();
			mDistanceBefore += tileSize;

			GameObject tile = tilePooler.GetObject();

			// Get the frontmost tile's position and adjust it
			Vector3 tilePosition = mTileList.Last.Value.transform.position;
			tilePosition.z = mDistanceAfter;
			tile.transform.position = tilePosition;
			tile.SetActive(true);

			Renderer tileRenderer = tile.GetComponentInChildren<Renderer>();
			Color color = tileRenderer.material.color;
			color.r = Random.Range(0.0f, 1.0f);
			color.g = Random.Range(0.0f, 1.0f);
			color.b = Random.Range(0.0f, 1.0f);
			tile.GetComponentInChildren<Renderer>().material.color = color;

			mTileList.AddLast(tile);

			mDistanceAfter += tileSize;
		}

	}
}
