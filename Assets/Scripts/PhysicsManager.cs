using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsManager : MonoBehaviour
{
    InputManager inputManager;
    Rigidbody playerRB;
    Animator animator;

    private float velocity;
    public bool isGrounded = false;

    private float gravity = -9.81f;
    public float gravityMod = 150.0f;
    private Vector3 gravityVector;

    public float jumpPower;
    private Vector3 jumpVector;

    public bool canJump = true;
    public bool canDoubleJump = false;

/*    public int numberOfJumps = 0;
    public int maxJumps = 2;*/


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ApplyGravity();
    }


    public void Jump()
    {
        if (canJump)
        {
            animator.SetBool("isJumping", true);
            playerRB.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            canJump = false;
            canDoubleJump = true;

        }

        if (canDoubleJump)
        {
            animator.SetBool("isJumping", true);
            playerRB.AddForce(transform.up * jumpPower * 1.3f, ForceMode.Impulse);
            canDoubleJump = false;
        }
    }

    private void ApplyGravity()
    {
        if (isGrounded && velocity < 0.0f)
        {
            velocity = -1.0f;
        }

        else
        {
            velocity += gravity * gravityMod * Time.deltaTime;
        }

        gravityVector.y = velocity;
        playerRB.AddForce(transform.up * gravityVector.y, ForceMode.Acceleration);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("isJumping", false);
            isGrounded = true;
            canJump = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}






















// old stuff from when floating sounded like a good idea
/*public class PhysicsManager: MonoBehaviour
{
    private Rigidbody rb;

    public GameObject capsule;

    private Ray ray;
    private bool _rayDidHit;

    public float rideHeight;
    private float gravity;
    private float spring;

    void CheckForColliders()
    {
        ray = new Ray(capsule.transform.position, (capsule.transform.up * -1.0f));
        Debug.DrawRay(capsule.transform.position, (capsule.transform.up * -1.0f) * rideHeight, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //_rayDidHit = true;
            //Debug.Log(hit.collider.gameObject.name + " was hit");
        }
    }

    void Update()
    {
        CheckForColliders();
    }
}
*/