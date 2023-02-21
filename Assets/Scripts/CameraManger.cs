using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManger : MonoBehaviour
{
    public Transform targetTransform;       //Object camera follows
    private Vector3 cameraFollowVelocity = Vector3.zero;

    public float CameraFollowSpeed = 0.2f;

    public float lookAngle;     //up and down
    public float pivotAngle;    //left and right


    public void Awake()
    {
        targetTransform = FindObjectOfType<PlayerManager>().transform;
    }

    public void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTransform.position, ref cameraFollowVelocity, CameraFollowSpeed);
      
        transform.position = targetPosition;
    }

    public void RotateCamera()
    {
        //lookAngle = lookAngle + joystick right x * cameralookspeed
        //pivotAngle = PivotAngle - joystic right y * camera pivot speed
    }
}
