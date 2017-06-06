/******************************************************************************
* 
* Class name: ObstacleAnimation
* Created by: Edgard Damiani
* Description: Creates the obstacle's wobbling animation 
* 
******************************************************************************/

using UnityEngine;
using System.Collections;

public class ObstacleAnimation : MonoBehaviour
{
	/*********************** Public properties *****************************/
	public bool isRotating = false;
	public bool isFloating = false;

	public Vector3 rotationAngle;
	public float rotationSpeed;

	public float floaterDuration = 1.0f;
	public float floatRange = 0.5f;


	/*********************** Private properties *****************************/
	private float mFloatTimer = 0;
	private bool mGoingUp = true;


	/*********************** Public methods *****************************/
	void Start()
	{

	}
	
	void Update()
	{		
		if(isRotating)
		{
			transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
		}

		if(isFloating)
		{
			mFloatTimer += Time.deltaTime;

			float result;

			if(mGoingUp)
			{
				result = Mathf.SmoothStep(-floatRange, floatRange, mFloatTimer / floaterDuration);
			}
			else
			{
				result = Mathf.SmoothStep(floatRange, -floatRange, mFloatTimer / floaterDuration);
			}

			Vector3 delta = Vector3.zero;
			delta.y = result;
			transform.Translate(delta);

			if (mFloatTimer >= floaterDuration)
			{
				mGoingUp = !mGoingUp;
				mFloatTimer = 0;
			}
		}
	}
}
