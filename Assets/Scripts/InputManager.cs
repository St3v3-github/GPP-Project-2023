using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    Animator animator;

    //Animator animator;
    //AnimatorManager animatorManager;

    public Vector2 movementInput;
    public Vector2 cameraInput;
    public bool jumpInput = false;
    public bool sprintInput = false;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizontalInput;


    private void Awake()
    {
        //old blend tree animation code
        //animatorManager = GetComponent<AnimatorManager>();  
        //animator = GetComponent<Animator>();

        animator = GetComponent<Animator>();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        if (horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("isRunning", true);
        }

        else
        {
            animator.SetBool("isRunning", false);
        }

        //moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        //animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            
            //Debug.Log("Jump = " + jumpInput);
        }

        else
        {
          
        }
        
    }

    private void HandleSprintInput()
    {
        if (sprintInput)
        {
            animator.SetBool("isSprinting", true);
            //Debug.Log("Sprint = " + sprintInput);
        }

        else
        {
            animator.SetBool("isSprinting", false);
        }
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
        HandleJumpInput();
        HandleSprintInput();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();

            playerControls.PlayerMovement.Jump.performed+= ctx => jumpInput = true;
            playerControls.PlayerMovement.Jump.canceled += ctx => jumpInput = false;

            playerControls.PlayerMovement.Sprint.started += ctx => sprintInput = true;
            playerControls.PlayerMovement.Sprint.canceled += ctx => sprintInput = false;
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
