/******************************************************************************
* 
* Class name: ObjectPooler
* Created by: Edgard Damiani
* Description: Creates and manages an object pool 
* 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	/*********************** Public properties *****************************/
	/// <summary>Number of objects to be initially created.</summary> 
	public int initialPool = 5;
	/// <summary>The object to be pooled.</summary> 
	public GameObject pooledObject;


	/*********************** Private properties *****************************/
	/// <summary>List of pooled GameObjects.</summary>
	private List<GameObject> mObjectPool = new List<GameObject>();


	/*********************** Public methods *****************************/
	/// <summary>Returns an object from the pool, or creates
	/// a new one if all objects are being used.</summary>
	public GameObject GetObject()
	{
		for(int i = 0; i < mObjectPool.Count; i++)
		{
			// The availability criteria is the object not being active
			if(!mObjectPool[i].activeInHierarchy)
			{
				mObjectPool[i].SetActive(true);

				return mObjectPool[i];
			}
		}

		// If no pooled object is available, create a new one
		GameObject newObject = GameObject.Instantiate(pooledObject);
		mObjectPool.Add(newObject);

		return newObject;
	}

	/*********************** Private methods *****************************/
	void Start()
	{
		for(int i = 0; i < initialPool; i++)
		{
			GameObject newObject = GameObject.Instantiate(pooledObject);

			// Object should be set as inactive to be considered available
			newObject.SetActive(false);

			mObjectPool.Add(newObject);
		}
	}
	
	void Update()
	{
		
	}
}
