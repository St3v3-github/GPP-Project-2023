using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PhysicsManager physicsManager;
    ButtonLogic buttonLogic;
    
    public GameObject cam;


    Rigidbody playerRB;
    Animator animator;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public bool jumpInput = false;
    public bool sprintInput = false;

    public bool SelectingInput = false;

    public bool FightingInput = false;
    public bool ChunLiInput = false;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizontalInput;


    private void Awake()
    {
        physicsManager = FindObjectOfType<PhysicsManager>();
        buttonLogic = FindObjectOfType<ButtonLogic>();
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        cam = GameObject.Find("CutsceneCam");
        cam.SetActive(false);
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleCameraInput();
        HandleJumpInput();
        HandleSprintInput();
        HandleFightingInput();
        HandleSelectInput();
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

    private void HandleJumpInput()
    {
        if (jumpInput) 
        {
            jumpInput = false;
            physicsManager.Jump();
        }
    }

    private void HandleSelectInput()
    {
        if (SelectingInput && buttonLogic.btnPressable)
        {
            cam.SetActive(true);
            buttonLogic.DoorOpen();
            SelectingInput = false;
        }

        else if (!buttonLogic.btnPressable)
        {
            cam.SetActive(false);
        }
    }

    private void HandleFightingInput()
    {
        if (FightingInput )
        {
            animator.Play("Base Layer.MMA Kick", 0, 0.25f);
        }

        else
        {
            animator.SetBool("IsFighting", false);
        }
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

            playerControls.PlayerMovement.Select.started += ctx => SelectingInput = true;
            playerControls.PlayerMovement.Select.canceled += ctx => SelectingInput = false;

            playerControls.PlayerMovement.Attack.started += ctx => FightingInput = true;
            playerControls.PlayerMovement.Attack.canceled += ctx => FightingInput = false;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
