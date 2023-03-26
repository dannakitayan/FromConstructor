using System.Collections;
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

	//Amounts of rotate position by axis; Величина поворота позиции по оси;
	float rotateAmountX;
	float rotateAmountY;

	//The vector created for movement; Вектор созданный для перемещения;
	Vector3 moveVector;

	void Start()
	{
		//The CharacterController initialisation; Инициализация CharacterController;
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

	void Update()
	{
		Movement();
		Rotate();

		//Move the player; 
		controller.Move(moveVector * Time.deltaTime);
	}
}
