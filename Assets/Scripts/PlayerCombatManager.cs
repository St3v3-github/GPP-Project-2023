using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager: MonoBehaviour
{
    InputManager inputManager;
    PlayerMotion playerMotion;
    EnemyManager enemyManager;

   public Rigidbody enemyRB;

    [SerializeField] private LayerMask Ground, Enemy;

    [SerializeField] private float attackRange;
    [SerializeField] private bool InAttackRange;

    [SerializeField] private float bonkUp;
    [SerializeField] private float bonkBack;

    [SerializeField] private float enemyHealth = 10;
    public GameObject enemyObject;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMotion = GetComponent<PlayerMotion>();
        enemyManager = FindObjectOfType<EnemyManager>();

        enemyRB = FindObjectOfType<EnemyManager>().GetComponent<Rigidbody>();  
    }

    private void Update()
    {
        if (enemyHealth < 0)
        {
            enemyObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        InAttackRange = Physics.CheckSphere(transform.position, attackRange, Enemy);

        if (InAttackRange && inputManager.FightingInput)
        {
            HandleAttack();
        }
    }

    private void HandleAttack()
    {
        enemyRB.AddForce(transform.up * bonkUp, ForceMode.Impulse);
        enemyRB.AddForce(transform.forward * - bonkBack, ForceMode.Impulse);

        enemyHealth--;

    }

}

