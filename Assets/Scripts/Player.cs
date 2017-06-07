/******************************************************************************
* 
* Class name: Player
* Created by: Edgard Damiani
* Description: Handles player's movement and energy  
* 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	/*********************** Public properties *****************************/
	/// <summary>Player's energy.</summary>
	public int					energy = 2;

	/// <summary>Player's gravity.</summary>
	public float				gravity	= 9.8f;

	/// <summary>How much should the player move laterally.</summary>
	public float				lateralMovementRange = 1.0f;

	/// <summary>How long the lateral movement will take.</summary>
	public float				lateralMovementDuration = 1.0f;

	/// <summary>Player's initial speed.</summary>
	public float				speed = 1;

	/// <summary>How much will the speed increase each second.</summary>
	public float				speedIncreaseRate = 0.2f;

	/// <summary>Reference to the GUI manager.</summary>
	public GuiManager			guiManager;


	/*********************** Private properties *****************************/
	/// <summary>Player's initial position on the X-axis.</summary>
	private float				mCenterX = 0;

	/// <summary>Reference to the CharacterController component.</summary>
	private CharacterController mCharacterController;

	/// <summary>Controls the lateral movement.</summary>
	private float				mLateralMovementTimer = 0;

	/// <summary>Checks if the player is going left (true) or right (false).</summary>
	private bool				mIsGoingLeft = false;

	/// <summary>Checks if the player is moving laterally.</summary>
	private bool				mIsMovingLaterally = false;

	/// <summary>Holds the player's relative movement.</summary>
	private Vector3				mMovement;

	/// <summary>Checks the player's position relative to the tile
	/// (-1 = left, 0 = center, 1 = right).</summary>
	private int					mPositionInTile = 0;


	/*********************** Public methods *****************************/
	public void DecreaseEnergy()
	{
		energy--;

		GetComponentInChildren<ParticleSystem>().Play();

		if(energy == 0)
		{
			transform.Find("PlayerMesh").gameObject.SetActive(false);

			guiManager.ShowGameOverMenu();
		}
	}

	/*********************** Private methods *****************************/
	private void Start()
	{
		mCharacterController = GetComponentInChildren<CharacterController>();

		// Defines the central position as the player's initial position
		mCenterX = transform.position.x;

		GetComponentInChildren<ParticleSystem>().Stop();
	}
	
	private void Update()
	{
		if(energy > 0)
		{
			float lateralMovement = Input.GetAxisRaw("Horizontal") * speed;

			// Touch capturing
			if(Input.GetMouseButtonDown(0))
			{
				if(Input.mousePosition.x < Screen.width / 2)
				{
					lateralMovement = -1;
				}
				else
				{
					lateralMovement = 1;
				}
			}

			// This conditional should only happen if the player is not moving laterally
			if(lateralMovement < 0 && !mIsMovingLaterally)
			{
				// Checks if the player can move to the left
				if(mPositionInTile > -1)
				{
					mIsMovingLaterally = true;
					mIsGoingLeft = true;

					mPositionInTile--;
				}
			}
			else if(lateralMovement > 0 && !mIsMovingLaterally)
			{
				// Checks if the player is moving to the right
				if(mPositionInTile < 1)
				{
					mIsMovingLaterally = true;
					mIsGoingLeft = false;

					mPositionInTile++;
				}
			}

			float result = 0;

			if(mIsMovingLaterally && mIsGoingLeft)
			{
				// Increments the player movement
				mLateralMovementTimer += Time.deltaTime;

				// SmoothStep ensures that the movement ends swiftly and correctly (at least in theory)
				result = Mathf.SmoothStep(mCenterX, mCenterX - lateralMovementRange, mLateralMovementTimer / lateralMovementDuration);
				result -= transform.position.x;

				if(mLateralMovementTimer / lateralMovementDuration >= 1)
				{
					mLateralMovementTimer = 0;

					// Finds the player's current center, so that the next lateral movement can be correctly calculated
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
			//mMovement.z = speed * Time.deltaTime;
			mMovement.z = Mathf.SmoothStep(0, speed * Time.deltaTime, 0.7f);
			mCharacterController.Move(mMovement);

			// Increments the speed
			speed += speedIncreaseRate * Time.deltaTime;
		}
	}
}
