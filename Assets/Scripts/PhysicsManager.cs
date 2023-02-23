using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager: MonoBehaviour
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
