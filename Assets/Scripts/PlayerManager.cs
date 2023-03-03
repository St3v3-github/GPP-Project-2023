using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager: MonoBehaviour
{
    InputManager inputManager;
    CameraManager cameraManager;
    PlayerMotion playerMotion;

    private void Awake()
    {
       inputManager= GetComponent<InputManager>();
       cameraManager= FindObjectOfType<CameraManager>();
       playerMotion= GetComponent<PlayerMotion>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerMotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }
}
