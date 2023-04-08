using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	//const float gravitySpeed = 0.5f; //The gravity constant; 

	[Header("Player movement speed")]
	[SerializeField] float speed;
	[Header("Mouse sensitivity")]
	[SerializeField] float mouseSensitivity;

	//The head by camera;
	[Header("Head")]
	public Camera HeroHead;

	//bool grounded;

	//The player character controller;
	CharacterController controller;

	//Amounts of rotate position by axis;
	float rotateAmountX;
	float rotateAmountY;

	//The vector created for movement; 
	Vector3 moveVector;

	public static Func<Vector3> onPositionGet;

    void Awake()
    {
		onPositionGet += GetPlayerPosition;
	}

    void OnDestroy()
    {
		onPositionGet -= GetPlayerPosition;
	}

    void Start()
	{
		//The CharacterController initialisation; 
		controller = GetComponent<CharacterController>();
		CursorLock(true);
	}

	//Were a cursor locked? 
	void CursorLock(bool state)
	{
		Cursor.visible = !state; 
		switch(state)
        {
			case true:
				Cursor.lockState = CursorLockMode.Locked;
				break;
			case false:
				Cursor.lockState = CursorLockMode.None;
				break;
		}
	}

	void Rotate()
	{
		//Setting the values; 
		rotateAmountX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		rotateAmountY += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		//Limit a rotation of the camera along by X axis;
		rotateAmountY += MainCameraShake.Angle;
		rotateAmountY = Mathf.Clamp(rotateAmountY, -60f, 60f);

		//Rotate player;
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotateAmountX, 0f);
        //Rotate camera;
        HeroHead.transform.rotation = Quaternion.Euler(-rotateAmountY, HeroHead.transform.rotation.eulerAngles.y, 0);
    }

	void Movement()
	{
		//grounded = controller.isGrounded; //Check the grunt is grounded; 

		moveVector.x = Input.GetAxis("Horizontal") * speed;

		//if (!grounded) moveVector.y -= gravitySpeed; //Grunt falling; 

		moveVector.z = Input.GetAxis("Vertical") * speed;
		moveVector = transform.TransformDirection(moveVector);
	}

	Vector3 GetPlayerPosition()
    {
		return transform.position;
    }

	void Update()
	{
		Movement();
		Rotate();

		//Move the player; 
		controller.Move(moveVector * Time.deltaTime);
	}
}
