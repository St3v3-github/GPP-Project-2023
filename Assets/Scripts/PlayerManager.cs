using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager: MonoBehaviour
{
    InputManager inputManager;
    PlayerMotion playerMotion;

    private void Awake()
    {
       inputManager= GetComponent<InputManager>();
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
}
