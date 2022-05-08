using System.Collections;
using UnityEngine;

public class Grunt : MonoBehaviour
{
	const float gravitySpeed = 0.5f; //The gravity constant; ��������� ����������;

	[Header("�������� �������� ������")]
	public float Speed;
	[Header("���������������� ����")]
	public float MouseSensitivity;

	//The head by camera;
	[Header("������")]
	public Camera HeroHead;

	bool grounded;

	//The grunt character controller;
	//���������� ��������� CharacterController ������� ������� �� ������� ������;
	CharacterController controller;

	//Amounts of rotate position by axis; �������� �������� ������� �� ���;
	float rotateAmountX;
	float rotateAmountY;

	//The vector created for movement; ������ ��������� ��� �����������;
	Vector3 moveVector;

	void Start()
	{
		//The CharacterController initialisation; ������������� CharacterController;
		controller = GetComponent<CharacterController>();
		CursorLock(true);
	}

	//Were a cursor locked? ��� �� ������ ������������?;
	void CursorLock(bool state)
	{
		Cursor.visible = !state; //��������� �������� ����;
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
		//Setting the values; ��������� ��������;
		rotateAmountX += Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
		rotateAmountY += Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

		//Limit a rotation of the camera along by X axis; ����������� �������� ������ �� ��� �;
		rotateAmountY += MainCameraShake.Angle;
		rotateAmountY = Mathf.Clamp(rotateAmountY, -60f, 60f);

		//Rotate grunt;
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotateAmountX, 0f);
        //Rotate camera;
        HeroHead.transform.rotation = Quaternion.Euler(-rotateAmountY, HeroHead.transform.rotation.eulerAngles.y, 0);
    }

	void Movement()
	{
		grounded = controller.isGrounded; //Check the grunt is grounded; ��������� �� ����� �� �����;

		moveVector.x = Input.GetAxis("Horizontal") * Speed;

		if (!grounded) moveVector.y -= gravitySpeed; //Grunt falling; ���� ����� �� �� �����, �� ������� �������;

		moveVector.z = Input.GetAxis("Vertical") * Speed;
		moveVector = transform.TransformDirection(moveVector);
	}

	void Update()
	{
		Movement();
		Rotate();

		//Move the grunt; ������� ������ � �������� ���������� ������;
		controller.Move(moveVector * Time.deltaTime);
	}
}
