/******************************************************************************
* 
* Class name: CameraMovement
* Created by: Edgard Damiani
* Description: Takes care of camera movement and chasing 
* 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	/*********************** Public properties *****************************/
	/// <summary>Distance to be kept from the followed object.</summary> 
	public float		distance = 5;

	/// <summary>The object that will be chased by the camera.</summary> 
	public Transform	objectToFollow;

	/// <summary>Angle around the X-axis in relation to the followed object's 
	/// horizontal plane.</summary> 
	public float		rotationInDegrees = 30;


	/*********************** Private properties *****************************/
	/// <summary>Camera's position.</summary>
	private Vector3		mPosition;


	/*********************** Private methods *****************************/
	void Start()
	{
		mPosition = objectToFollow.position;
		mPosition.z = -distance;

		transform.position = mPosition;

		transform.RotateAround(objectToFollow.position, objectToFollow.rotation * Vector3.right, rotationInDegrees);
		transform.LookAt(objectToFollow);
	}
	
	void Update()
	{
		transform.LookAt(objectToFollow);

		Vector3 pointToFollow = objectToFollow.position;
		pointToFollow.y = transform.position.y;

		Vector3 direction = transform.position - pointToFollow;

		float currentDistance = direction.magnitude;

		if(currentDistance > distance * 1.2)
		{
			float newDistance = Mathf.SmoothStep(currentDistance, distance, (1 - (distance / currentDistance)) * 0.8f);
			Vector3 directionTimesDistance = direction.normalized * newDistance;
			Vector3 position = pointToFollow + directionTimesDistance;

			transform.position = position;
		}
	}
}
