using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager3 : MonoBehaviour
{
    public GameObject player;
    [SerializeField] Vector3 newPos, velocity;
    [SerializeField] private float speed, timer, detectionRad = 20;
    [SerializeField] private bool isMoving, playerDetected;

    // Start is called before the first frame update
    void Awake()
    {
        newPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        PlayerDetection();
    }

    void HandleMovement()
    {
        if (transform.position == newPos)
        {
            timer = timer + Time.deltaTime;
            if (timer >= 2)
            {
                newPos = new Vector3(transform.position.x + Random.Range(-20, 20), transform.position.y, transform.position.z + Random.Range(-20, 20));
            }
        }
        else
        {
            timer = 0;
            var lookPos = newPos - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, Time.deltaTime, speed);
        }
    }

    void PlayerDetection()
    {
        // finds distance between player & enemy
        var dist = Vector3.Distance(player.transform.position, transform.position);

        //checks to see if player is near enemy
        if (dist <= detectionRad)
        {
            playerDetected = true;
            newPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        }
        else
        {
            playerDetected = false;
        }
    }
}
