using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController3 : MonoBehaviour
{
    private Rigidbody rb;
    PlayerControls controls;
    Vector2 move;
    public float speed = 10;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;

        //Debugging
        //controls.Player.Jump.performed += ctx => SendMessage();
        //controls.Player.Move.performed += ctx => SendMessage(ctx.ReadValue<Vector2>());
        //controls.Player.Look.performed += ctx => SendMessage(ctx.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }


    private void OnDisable()
    {
        controls.Player.Disable();
    }


    void SendMessage(Vector2 coordinates)
    {
        Debug.Log("Thumb-stick coordinates = " + coordinates);
    }

    void SendMessage()
    {
        Debug.Log("Button Pressed");
    }


    void FixedUpdate()
    {
        Vector3 movement = new Vector3(move.x, 0.0f, move.y) * speed * Time.deltaTime;
        rb.AddForce(movement * speed);
    }


    void Update()
    {

    }
}