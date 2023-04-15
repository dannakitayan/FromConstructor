using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    GameInput inputActions;
    Vector2 movementVector = Vector2.zero;
    Vector2 viewVector = Vector2.zero;

    public static Func<Vector2> GetMovement;
    public static Func<Vector2> GetView;
    public static Action onUse;
    public static Action onShoot;

    void Awake()
    {
        GetMovement += () => movementVector;
        GetView += () => viewVector;

        inputActions = new GameInput();
        SubscribePlayerActions();

        SwitchInputState(InputState.Player);
    }

    void OnDestroy()
    {
        GetMovement -= () => movementVector;
        GetView -= () => viewVector;
    }

    void SwitchInputState(InputState state)
    {
        switch(state)
        {
            case InputState.Menu:
                inputActions.Sound.Disable();
                inputActions.Player.Disable();
                inputActions.Menu.Enable();
                break;
            case InputState.Sound:
                inputActions.Player.Disable();
                inputActions.Menu.Disable();
                inputActions.Sound.Enable();
                break;
            case InputState.Player:
                inputActions.Menu.Disable();
                inputActions.Sound.Disable();
                inputActions.Player.Enable();
                break;
        }
    }

    void SubscribePlayerActions()
    {
        inputActions.Player.Movement.performed += Movement;
        inputActions.Player.Movement.canceled += Movement;
        inputActions.Player.View.performed += View;
        inputActions.Player.Use.canceled += PlayerUse;
        inputActions.Player.Shoot.performed += Shoot;
    }

    private void View(InputAction.CallbackContext obj)
    {
        switch (obj.phase)
        {
            case InputActionPhase.Performed:
                viewVector = obj.ReadValue<Vector2>();
                break;
        }
    }

    private void Movement(InputAction.CallbackContext obj)
    {
        switch (obj.phase)
        {
            case InputActionPhase.Performed:
                movementVector = obj.ReadValue<Vector2>();
                break;
            case InputActionPhase.Canceled:
                movementVector = obj.ReadValue<Vector2>();
                break;
        }
    }

    void PlayerUse(InputAction.CallbackContext obj)
    {
        switch (obj.phase)
        {
            case InputActionPhase.Canceled:
                onUse?.Invoke();
                break;
        }
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        switch (obj.phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("Shoot");
                onShoot?.Invoke();
                break;
        }
    }
}
