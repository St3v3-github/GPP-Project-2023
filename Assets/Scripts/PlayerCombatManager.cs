using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerMotion playerMotion;
    EnemyManager enemyManager;
    EnemyVisualManager enemyVisualManager;
    BossVisualManager bossVisualManager;

    public Rigidbody enemyRB;
    public Rigidbody BossRB;


    [SerializeField] private LayerMask Ground, Enemy, Boss;

    [SerializeField] private float attackRange;
    [SerializeField] private bool InAttackRange;
    [SerializeField] private bool InBossAttackRange;

    [SerializeField] private float bonkUp;
    [SerializeField] private float bonkBack;

    [SerializeField] private float enemyHealth = 10;
    public GameObject enemyObject;

    [SerializeField] private float bossHealth = 10;
    public GameObject BossObject;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMotion = GetComponent<PlayerMotion>();
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyVisualManager = FindObjectOfType<EnemyVisualManager>();
        bossVisualManager = FindObjectOfType<BossVisualManager>();


        /*        enemyRB = FindObjectOfType<EnemyManager>().GetComponent<Rigidbody>();
                BossRB = FindObjectOfType<EnemyManager>().GetComponent<Rigidbody>();*/

    }

    private void Update()
    {
        if (enemyHealth < 0)
        {
            enemyObject.SetActive(false);
        }

        if (bossHealth < 0)
        {
            BossObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        InAttackRange = Physics.CheckSphere(transform.position, attackRange, Enemy);

        if (InAttackRange && inputManager.FightingInput)
        {
            HandleAttack();
        }

        InBossAttackRange = Physics.CheckSphere(transform.position, attackRange, Boss);

        if (InBossAttackRange && inputManager.FightingInput)
        {
            HandleBossAttack();
        }
    }

    private void HandleAttack()
    {
        enemyRB.AddForce(transform.up * bonkUp, ForceMode.Impulse);
        enemyRB.AddForce(transform.forward * -bonkBack, ForceMode.Impulse);

        enemyHealth--;
        enemyVisualManager.Damage();

    }

    private void HandleBossAttack()
    {
        BossRB.AddForce(transform.up * bonkUp, ForceMode.Impulse);
        BossRB.AddForce(transform.forward * -bonkBack, ForceMode.Impulse);

        bossHealth--;
        bossVisualManager.Damage();

    }
}
