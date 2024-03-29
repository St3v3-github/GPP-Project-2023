using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;
    SplineManager splineManager;
    LockOnCam lockOnCam;

    public Transform targetTransform;       //Object camera follows
    public Transform cameraPivot;             //Object camera pivots on
    public Transform cameraTransform;    //Transform of actual camera object
    public LayerMask collisionLayers;        //Layers we want camera to collide with
    private float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;

    [SerializeField] private GameObject player;

    public float cameraCollisionOffset = 0.2f;       //how much camera will jump off objects.
    public float minimumCollisionOffset = 0.2f;
    public float cameraCollisionRadius = 0.2f;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 5f;
    public float cameraPivotSpeed = 5f;

    public float lookAngle;     //up and down
    public float pivotAngle;    //left and right
    public float minPivotAngle = -35;
    public float maxPivotAngle = 35;

    public void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        splineManager = FindObjectOfType<SplineManager>();
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;

        lockOnCam = FindObjectOfType<LockOnCam>();

    }

    public void HandleAllCameraMovement()
    {
        if (!splineManager.InBox)
        {
            RotateCamera();
            FollowTarget();
            HandleCameraCollisions();

            /*            if (inputManager.LockOnInput)
                        {
                            lockOnCam.enabled = true;
                        }

                        else
                        {
                            lockOnCam.enabled = false;
                        }*/

        }

        else
        {
            splineRules();

        }
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast
            (cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = -(distance - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = targetPosition - minimumCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }

    private void splineRules()
    {
        Vector3 lookAtPosition = targetTransform.position + transform.up * 1.8f;
        var targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
    }
}

   /* private void splineRules()
    {
        Vector3 lookAtPosition = player.transform.position + transform.up * 1.8f;
        var targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
    }
}*/
