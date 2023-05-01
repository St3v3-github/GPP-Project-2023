using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyManager2 : MonoBehaviour
{
    //GameObjects
    public GameObject player;
    public GameObject enemy;

    //Enemy Data
    [SerializeField] private float moveSpeed, rotateSpeed;
    [SerializeField] private Vector3 velocity;

    //Location Data
    [SerializeField] private Vector3 targetTransform;
    [SerializeField] private float moveRange, pause;
    [SerializeField] private bool moved;

    //CheckSpheres
    [SerializeField] private float sightRange;
    [SerializeField] private bool inSightRange;
    [SerializeField] private LayerMask Player, Ground;


    private void Awake()
    {

    }

    private void Update()
    {
        inSightRange = Physics.CheckSphere(transform.position, sightRange, Player);

        if (!inSightRange)
        {
            Patrol();
        }

        if (inSightRange)
        {
            Hunt();
        }

    }

    private void Patrol()
    {
        if (!moved)
        {
            //Assign Random Position
            targetTransform = new Vector3(
                transform.position.x + Random.Range(-moveRange, moveRange),
                transform.position.y,
                transform.position.z + Random.Range(-moveRange, moveRange));

            //assign suitable forward vector
            Vector3 lookAtPosition = targetTransform;
            var targetRotation = Quaternion.LookRotation(lookAtPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            //Move
            Vector3 patrolPosition =
                Vector3.SmoothDamp(transform.position, targetTransform, ref velocity, moveSpeed);

            transform.position = patrolPosition;

            moved = true;
            Invoke(nameof(Patrol), pause);
        }
    }

    private void ResetPatrol()
    {
        moved = false;
    }

    private void Hunt()
    {
        //Assign Random Position
        targetTransform = player.transform.position;

        //assign suitable forward vector
        Vector3 lookAtPosition = targetTransform;
        var targetRotation = Quaternion.LookRotation(lookAtPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        //Move
        Vector3 huntingPosition =
            Vector3.SmoothDamp(transform.position, targetTransform, ref velocity, moveSpeed);

        transform.position = huntingPosition;

    }
}
