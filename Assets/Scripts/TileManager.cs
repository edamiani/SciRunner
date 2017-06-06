/******************************************************************************
* 
* Class name: TileManager
* Created by: Edgard Damiani
* Description: Manages the creation and destruction of tiles
* 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
	/*********************** Private methods *****************************/
	/// <summary>Object's Transform that will be used to track tiles' 
	/// life span.</summary>
	public Transform	targetToTrack;

	/// <summary>The object pooler that will hold the tiles.</summary>
	public ObjectPooler	tilePooler;

	/// <summary>Tile size in the Z-axis.</summary>
	public float		tileSize = 1;

	/// <summary>Number of tiles to be kept behind the 
	/// tracked object.</summary>
	public int			tilesBeforePlayer = 2;

	/// <summary>Number of tiles to be created ahead of the 
	/// tracked object.</summary>
	public int			tilesAfterPlayer = 5;


	/*********************** Private methods *****************************/
	/// <summary>Holds the distance that should be traversed by the
	/// tracked object so that new tiles are creatd.</summary>
	private float		mDistanceAfter;

	/// <summary>Holds the distance that should be traversed by the
	/// tracked object so that old tiles are removed.</summary>
	private float		mDistanceBefore;

	/// <summary>A doubly-linked list of tiles.</summary>
	private LinkedList<GameObject> 
						mTileList = new LinkedList<GameObject>();


	/*********************** Private methods *****************************/
	private void Start ()
	{
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
	
	private void Update ()
	{
		Vector3 targetPosition = targetToTrack.position;

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
