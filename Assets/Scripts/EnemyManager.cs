using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    Vector3 moveDirection;
    Transform playerTransform;
    Rigidbody enemyRB;

    public Vector3 targetPosition;

    public LayerMask Ground, Player;

    private Vector3 enemyVelocity = Vector3.zero;
    public float enemySpeed = .5f;
    public float rotationSpeed = 10.0f;

    //patroling
    public Vector3 targetSpot;
    bool targetSpotSet;
    public float targetSpotRange;

    //attacking 
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;



    private void Awake()
    {
        //player = GameObject.Find("Player").transform;
        //agent = GetComponent<NavMeshAgent>();

        enemyRB = GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Player").transform;

    }

    private void Update()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) Chasing();
        //if (playerInSightRange && playerInAttackRange) Attacking();
    }

    private void Patrolling()
    {
        if (!targetSpotSet) SearchTargetSpot();

        if (targetSpotSet)
        {
            Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetSpot, ref enemyVelocity, enemySpeed);

            transform.position = targetPosition;
        }

        Vector3 distanceToTargetSpot = transform.position - targetSpot;

        //walkpoint reached
        if (distanceToTargetSpot.magnitude < 1f)
            targetSpotSet = false;

    }

    private void SearchTargetSpot()
    {
        //calculate random point in range 
        float randomZ = Random.Range(-targetSpotRange, targetSpotRange);
        float randomX = Random.Range(-targetSpotRange, targetSpotRange);

        targetSpot = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(targetSpot, -transform.up, 2f, Ground))
            targetSpotSet = true;
    }

    private void Chasing()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
        (transform.position, playerTransform.position, ref enemyVelocity, enemySpeed);

        transform.position = targetPosition;
    }
}

/*    private void Attacking()
    {
        ai.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //attack code here


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    };*/

