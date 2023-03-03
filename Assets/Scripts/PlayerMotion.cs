using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion: MonoBehaviour
{
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRB;
    Animator animator;

    public float movementSpeed = 10.0f;
    public float rotationSpeed = 10.0f;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation(); 
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRB.velocity = movementVelocity;

        if (playerRB.velocity.x != 0 || playerRB.velocity.y != 0)
        {
            animator.SetBool("isRunning", true);
        }

        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero) 
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
     }
}
