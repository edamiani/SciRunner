using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int					energy = 2;
	public float				gravity	= 9.8f;
	public float				lateralMovementRange = 1.0f;
	public float				lateralMovementDuration = 1.0f;
	public float				speed		= 1;

	private float				mCenterX = 0;
	private CharacterController mCharacterController;
	private float				mLateralMovementTimer = 0;
	private bool				mIsGoingLeft = false;
	private bool				mIsMovingLaterally = false;
	private Vector3				mMovement;
	private int					mTilePosition = 0;

	void Start()
	{
		mCharacterController = GetComponentInChildren<CharacterController>();

		mCenterX = transform.position.x;

		GetComponentInChildren<ParticleSystem>().Stop();
	}
	
	void Update()
	{
		if(energy > 0)
		{
			float lateralMovement = Input.GetAxisRaw("Horizontal") * speed;

			if(lateralMovement < 0 && !mIsMovingLaterally)
			{
				if(mTilePosition > -1)
				{
					mIsMovingLaterally = true;
					mIsGoingLeft = true;

					mTilePosition--;
				}
			}
			else if(lateralMovement > 0 && !mIsMovingLaterally)
			{
				if(mTilePosition < 1)
				{
					mIsMovingLaterally = true;
					mIsGoingLeft = false;

					mTilePosition++;
				}
			}

			float result = 0;

			if(mIsMovingLaterally && mIsGoingLeft)
			{
				mLateralMovementTimer += Time.deltaTime;

				result = Mathf.SmoothStep(mCenterX, mCenterX - lateralMovementRange, mLateralMovementTimer / lateralMovementDuration);
				result -= transform.position.x;

				if(mLateralMovementTimer / lateralMovementDuration >= 1)
				{
					mLateralMovementTimer = 0;
					mCenterX = transform.position.x;

					mIsMovingLaterally = false;
				}
			}
			else if(mIsMovingLaterally && !mIsGoingLeft)
			{
				mLateralMovementTimer += Time.deltaTime;

				result = Mathf.SmoothStep(mCenterX, mCenterX + lateralMovementRange, mLateralMovementTimer / lateralMovementDuration);
				result -= transform.position.x;

				if(mLateralMovementTimer / lateralMovementDuration >= 1)
				{
					mLateralMovementTimer = 0;
					mCenterX = transform.position.x;

					mIsMovingLaterally = false;
				}
			}

			mMovement.x = result;
			mMovement.y = -gravity * speed * Time.deltaTime;
			mMovement.z = speed * Time.deltaTime;
			mCharacterController.Move(mMovement);
		}
	}

	public void DecreaseEnergy()
	{
		energy--;

		GetComponentInChildren<ParticleSystem>().Play();

		if(energy == 0)
		{
			transform.Find("PlayerMesh").gameObject.SetActive(false);
		}
	}
}
