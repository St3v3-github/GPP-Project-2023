using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Rigidbody enemyRB;
    [SerializeField] private Rigidbody playerRB;

    [SerializeField] private LayerMask Ground, Player;

    [SerializeField] private float enemySpeed = 10f;
    private Vector3 enemyVelocity = Vector3.zero;
    private Vector3 moveDirection;

    //patroling
     [SerializeField] private Vector3 patrolSpot;
    [SerializeField] private bool patrolSpotSet = false;
    [SerializeField] private float patrolSpotRange;

    //hunting
    [SerializeField] private Vector3 huntingSpot;
    [SerializeField] private bool huntingSpotSet = false;

    //attacking 
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private bool alreadyAttacked;

    //states
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
            huntingSpotSet = false;
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            Hunting();
            patrolSpotSet = false;
        }

        if (playerInSightRange && playerInAttackRange)
        {
            Hunting();
            Attacking();
        }
    }

    private void Patrolling()
    {

        if (!patrolSpotSet) ChoosePatrolSpot();

        if (patrolSpotSet)
        {
            Vector3 patrolPosition = Vector3.SmoothDamp
            (transform.position, patrolSpot, ref enemyVelocity, enemySpeed);

            transform.position = patrolPosition;
        }

        Vector3 distanceToPatrolSpot = transform.position - patrolSpot;

        //walkpoint reached
        if (distanceToPatrolSpot.magnitude < 1f)
            patrolSpotSet = false;

    }

    private void ChoosePatrolSpot()
    {
        enemyVelocity = Vector3.zero;

        //calculate random point in range 
        float randomZ = Random.Range(-patrolSpotRange, patrolSpotRange);
        float randomX = Random.Range(-patrolSpotRange, patrolSpotRange);

        patrolSpot = new Vector3(transform.position.x * randomX, transform.position.y, transform.position.z * randomZ);

        if (Physics.Raycast(patrolSpot, -transform.up, 2f, Ground))
            patrolSpotSet = true;
    }

    private void ChooseHuntingSpot()
    {
        enemyVelocity = Vector3.zero;

        huntingSpot = new Vector3(playerRB.transform.position.x, transform.position.y, playerRB.transform.position.z);

        if (Physics.Raycast(huntingSpot, -transform.up, 2f, Ground))
            huntingSpotSet = true;
    }



    private void Hunting()
    {
        /*        Vector3 targetPosition = Vector3.SmoothDamp
                (transform.position, playerTransform.position, ref enemyVelocity, enemySpeed);

                transform.position = targetPosition;*/

        //transform.Translate(playerTransform.position);

      
        if (!huntingSpotSet) ChooseHuntingSpot();

        if (huntingSpotSet)
        {
            Vector3 huntingPosition = Vector3.SmoothDamp
            (transform.position, huntingSpot, ref enemyVelocity, enemySpeed);

            transform.position = huntingPosition;
        }

        Vector3 distanceToHuntingSpot = transform.position - huntingSpot;

        //walkpoint reached
        if (distanceToHuntingSpot.magnitude < 1f)
            huntingSpotSet = false;
    }

private void Attacking()
    {
        if (!alreadyAttacked)
        {

            playerRB.AddForce(0, 100, 0, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

        }

        if (alreadyAttacked) 
        Debug.Log("Hit");
    }

    private void OnCollisionExit(Collision collision)
    {

    }


}





////Attempt 2
///

/*public class EnemyManager : MonoBehaviour
{
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        enemyRB = GetComponent<Rigidbody>();
    }

    Public Void HandleAllStates()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
            huntingSpotSet = false;
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            Hunting();
            patrolSpotSet = false;
        }

        if (playerInSightRange && playerInAttackRange)
        {
            Hunting();
            Attacking();
        }
    }
}*/
