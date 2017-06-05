using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public GameObject pooledObject;

	private List<GameObject> mObjectPool;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public GameObject GetObject()
	{
		for(int i = 0; i < mObjectPool.Count; i++)
		{
			if(!mObjectPool[i].activeInHierarchy)
			{
				mObjectPool[i].SetActive(true);

				return mObjectPool[i];
			}
		}

		GameObject newObject = GameObject.Instantiate(pooledObject);
		mObjectPool.Add(newObject);

		return newObject;
	}

	public void Initialize()
	{
		mObjectPool = new List<GameObject>();

		for(int i = 0; i < 5; i++)
		{
			GameObject newObject = GameObject.Instantiate(pooledObject);
			newObject.SetActive(false);
			mObjectPool.Add(newObject);
		}
	}
}
