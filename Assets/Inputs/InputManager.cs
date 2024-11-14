using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 movement;

    private PlayerInput playerInput;
    private InputAction movementAction;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        movementAction = playerInput.actions["Movement"];
    }

    private void Update()
    {
        movement = movementAction.ReadValue<Vector2>();
    }
}
