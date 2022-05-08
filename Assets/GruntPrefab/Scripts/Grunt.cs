using System.Collections;
using UnityEngine;

public class Grunt : MonoBehaviour
{
	const float gravitySpeed = 0.5f; //The gravity constant; Константа гравитации;

	[Header("Скорость движения игрока")]
	public float Speed;
	[Header("Чувствительность мыши")]
	public float MouseSensitivity;

	//The head by camera;
	[Header("Камера")]
	public Camera HeroHead;

	bool grounded;

	//The grunt character controller;
	//Использует компонент CharacterController который имеется на объекте игрока;
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

	//Were a cursor locked? Был ли курсор заблокирован?;
	void CursorLock(bool state)
	{
		Cursor.visible = !state; //Видимость курсорас мыши;
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
		//Setting the values; Установка значений;
		rotateAmountX += Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
		rotateAmountY += Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

		//Limit a rotation of the camera along by X axis; Ограничение вращения камеры по оси Х;
		rotateAmountY += MainCameraShake.Angle;
		rotateAmountY = Mathf.Clamp(rotateAmountY, -60f, 60f);

		//Rotate grunt;
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotateAmountX, 0f);
        //Rotate camera;
        HeroHead.transform.rotation = Quaternion.Euler(-rotateAmountY, HeroHead.transform.rotation.eulerAngles.y, 0);
    }

	void Movement()
	{
		grounded = controller.isGrounded; //Check the grunt is grounded; Проверяет на замле ли игрок;

		moveVector.x = Input.GetAxis("Horizontal") * Speed;

		if (!grounded) moveVector.y -= gravitySpeed; //Grunt falling; Если игрок не на земле, то врубает падение;

		moveVector.z = Input.GetAxis("Vertical") * Speed;
		moveVector = transform.TransformDirection(moveVector);
	}

	void Update()
	{
		Movement();
		Rotate();

		//Move the grunt; Двигаем гранта в процессе обновления кадров;
		controller.Move(moveVector * Time.deltaTime);
	}
}
