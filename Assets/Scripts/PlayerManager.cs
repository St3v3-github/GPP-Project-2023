using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager: MonoBehaviour
{
    InputManager inputManager;
    CameraManger cameraManager;
    PlayerMotion playerMotion;

    private void Awake()
    {
       inputManager= GetComponent<InputManager>();
       cameraManager= FindObjectOfType<CameraManger>();
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
        cameraManager.FollowTarget();
    }
}
