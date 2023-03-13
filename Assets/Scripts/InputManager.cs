using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    public Rigidbody playerRB;
    Animator animator;

    //Animator animator;
    //AnimatorManager animatorManager;

    public Vector2 movementInput;
    public Vector2 cameraInput;

/*    public bool jumpInput = false;
    public bool onGround = true;
    public float jumpHeight = 10f;*/

/*    public float airTime = 0.1f;
    public float fallVelocity = 1f;*/

    public bool sprintInput = false;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizontalInput;


    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleCameraInput();
        //HandleJumpInput();
        HandleSprintInput();
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
    }


    private void HandleCameraInput()
    {
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;
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


    /*    private void HandleJumpInput()
        {
            playerRB.AddForce(-Vector3.up * fallVelocity); //* airTime);

            if (jumpInput && onGround)
            {
                onGround = false;
                animator.SetBool("isJumping", true);
                playerRB.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Acceleration);
            }        
        }*/

/*    private void HandleJumpInput()
    {
        animator.SetBool("isJumping", true);
    }

    private void onCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("isJumping", false);
            onGround = true;
        }
    }*/


    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();

            //playerControls.PlayerMovement.Jump.performed+= ctx => jumpInput = true;
            //playerControls.PlayerMovement.Jump.canceled += ctx => jumpInput = false;

            playerControls.PlayerMovement.Sprint.started += ctx => sprintInput = true;
            playerControls.PlayerMovement.Sprint.canceled += ctx => sprintInput = false;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
