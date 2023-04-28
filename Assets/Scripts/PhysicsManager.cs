using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsManager : MonoBehaviour
{
    InputManager inputManager;
    Powerup powerup;

    Rigidbody playerRB;
    Animator animator;

    private float velocity;
    public bool isGrounded = false;

    private float gravity = -9.81f;
    public float gravityMod = 1;
    private Vector3 gravityVector;

    public float jumpPower;
    private Vector3 jumpVector;
    public int jumps = 0;


    //public float doubleJumpMod = 2f;

    //public bool canJump = true;
    //public bool canDoubleJump = false;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        powerup = FindObjectOfType<Powerup>();
    }

/*    private void FixedUpdate()
    {
        ApplyGravity();
    }*/


    public void Jump()
    {
        if (isGrounded)
        {
            animator.SetBool("isJumping", true);
            playerRB.AddForce(transform.up * jumpPower);
           
        }

        if (powerup.aquired == true)
        {
            if (jumps == 1)
            {
                animator.Play("Base Layer.Idle Jump 0", 0, 0);
                playerRB.AddForce(transform.up * jumpPower);
                jumps++;
            }
        }
        /*        {
            canDoubleJump = true;
        }*/
        /* if (canDoubleJump)
         {
             //animator.SetBool("isJumping", true);
             animator.Play("Base Layer.Idle Jump 0", 0, 0);
             playerRB.AddForce(transform.up * jumpPower * doubleJumpMod , ForceMode.Impulse);
             canDoubleJump = false;
         }*/
    }

    private void ApplyGravity()
    {
        if (isGrounded && velocity < 0.0f) {
            velocity = -1.0f; }

        else { velocity += gravity * gravityMod; }

        gravityVector.y = velocity;
        playerRB.AddForce(transform.up * gravityVector.y);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") //&& powerup.aquired)
        {
            animator.SetBool("isJumping", false);
            isGrounded = true;
            jumps = 0;
   
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        jumps++;
       
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