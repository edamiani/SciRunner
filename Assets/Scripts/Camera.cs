using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	public float		distance = 5;
	public Transform	objectToFollow;
	public float		rotationInDegrees = 30;

	private Vector3		mPosition;

	void Start ()
	{
		mPosition = objectToFollow.position;
		mPosition.z = -distance;

		transform.position = mPosition;

		transform.RotateAround(objectToFollow.position, objectToFollow.rotation * Vector3.right, rotationInDegrees);
		transform.LookAt(objectToFollow);
	}
	
	void Update ()
	{
		transform.LookAt(objectToFollow);

		Vector3 direction = objectToFollow.position - transform.position;

		if(direction.magnitude > distance)
		{
			transform.position = Vector3.Lerp(transform.position, direction.normalized * Time.deltaTime, 0.5f);
		}
	}
}
