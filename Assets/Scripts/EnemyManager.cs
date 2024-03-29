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
    private Vector3 enemyVelocity;
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
    [SerializeField] private bool playerInSightRange, playerInAttackRange, moving = false;

    [SerializeField] private float waitTime = 2;


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
            if (!moving)
            { 
               StartCoroutine(Patrolling()); 
            }

            //huntingSpotSet = false;
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            Hunting();
           // patrolSpotSet = false;

            //HandleEnemyRotation();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            Hunting();
            Attacking();

            //HandleEnemyRotation();
        }
    }

    /*    public void HandleEnemyRotation()
        {
            Vector3 lookAtPosition = playerRB.transform.position + transform.up * 1.8f;
            var targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        }*/

    IEnumerator Patrolling()
    {
        moving = true;
        Vector3 lookAtPosition = patrolSpot;
        var targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

        enemyRB.AddForce(transform.forward * enemySpeed);

        if(patrolSpotSet)
        {
            Vector3 distanceToPatrolSpot = transform.position - patrolSpot;
            if (distanceToPatrolSpot.magnitude < 1f)
            {
                patrolSpotSet = false;
                moving = false;
            }
        }

        yield return new WaitForSeconds(waitTime);

        float randomZ = Random.Range(-patrolSpotRange * 2, patrolSpotRange);
        float randomX = Random.Range(-patrolSpotRange * 2, patrolSpotRange);

        patrolSpot = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        patrolSpotSet = true;

    }
        private void HuntMove()
        {
            if (huntingSpotSet)
            {
                Vector3 distanceToHuntinglSpot = transform.position - huntingSpot;
                if (distanceToHuntinglSpot.magnitude < 1f)
                {
                    huntingSpotSet = false;
                }
            }
         }


    private void Hunting()
    {
        Vector3 lookAtPosition = playerRB.transform.position;
        var targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

        enemyRB.AddForce(transform.forward * enemySpeed);
    }


    /*    private void ChooseHuntingSpot()
        {
            enemyVelocity = Vector3.zero;

            huntingSpot = new Vector3(playerRB.transform.position.x, transform.position.y, playerRB.transform.position.z);

            if (Physics.Raycast(huntingSpot, -transform.up, 2f, Ground))
                huntingSpotSet = true;
        }*/



    /*    private void Hunting()
        {
            *//*        Vector3 targetPosition = Vector3.SmoothDamp
                    (transform.position, playerTransform.position, ref enemyVelocity, enemySpeed);

                    transform.position = targetPosition;*//*

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
        }*/

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
        patrolSpotSet = false;

        if (alreadyAttacked) { Debug.Log("Hit"); }
           
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
