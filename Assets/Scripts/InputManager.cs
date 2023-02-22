using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizontalInput;

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    private void HandleCameraInput()
    {
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleCameraInput();
        //HandleJumpInput
        //HandleActionInput
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

/*    private void SendMessage(Vector2 coordinates)
    {
        Debug.Log("Thumb-stick coordinates = " + coordinates);
    }*/
}
