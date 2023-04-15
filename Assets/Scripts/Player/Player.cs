using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	//const float gravitySpeed = 0.5f; //The gravity constant; 

	[Header("Player movement speed")]
	[SerializeField] float speed;
	[Header("Mouse sensitivity")]
	[SerializeField] float mouseSensitivity;

	[Header("Head")]
	public Camera HeroHead;

	//bool grounded;

	CharacterController controller;

	float rotateAmountX;
	float rotateAmountY;

	Vector3 moveVector;

	public static Func<Vector3> onPositionGet;

    void Awake()
    {
		controller = GetComponent<CharacterController>();
		onPositionGet += GetPlayerPosition;
	}

    void OnDestroy()
    {
		onPositionGet -= GetPlayerPosition;
	}

    void Start()
	{
		
		CursorLock(true);
	}

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
		rotateAmountX += InputController.GetView.Invoke().x * mouseSensitivity * Time.deltaTime;
		rotateAmountY += InputController.GetView.Invoke().y * mouseSensitivity * Time.deltaTime;

		rotateAmountY += MainCameraShake.Angle;
		rotateAmountY = Mathf.Clamp(rotateAmountY, -60f, 60f);

		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotateAmountX, 0f);
        HeroHead.transform.rotation = Quaternion.Euler(-rotateAmountY, HeroHead.transform.rotation.eulerAngles.y, 0);
    }

	void Movement()
	{
		//grounded = controller.isGrounded; //Check the grunt is grounded; 

		moveVector.x = InputController.GetMovement.Invoke().x * speed;

		//if (!grounded) moveVector.y -= gravitySpeed; //Grunt falling; 

		moveVector.z = InputController.GetMovement.Invoke().y * speed;
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

        controller.Move(moveVector * Time.deltaTime);
	}
}
