using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float gravity	= 9.8f;
	public float speed		= 1;

	private CharacterController mCharacterController;
	private Vector3				mMovement;

	void Start()
	{
		mCharacterController = GetComponent<CharacterController>();
	}
	
	void Update()
	{
		mMovement.x = Input.GetAxisRaw("Horizontal") * speed;
		mMovement.y = -gravity * speed;
		mMovement.z = speed;
		//mCharacterController.Move(mMovement * Time.deltaTime);
	}
}
