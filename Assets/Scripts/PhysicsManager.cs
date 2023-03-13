using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    Rigidbody playerRB;

    
    private float gravity = -9.81f;
    public float gravityMod = 150.0f;
    private float velocity;
    private Vector3 gravityVector;


    public bool isGrounded;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ApplyGravity();
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
            isGrounded = true;
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