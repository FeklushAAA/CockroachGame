using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    [Header ("Подключение объектов")]

    [Space (10)]

    [SerializeField] private ShootingCharacteristics _shooting; //Ссылка на значения характеристик из другого скрипта
    
    [SerializeField] private Rigidbody _enemyAmmo;

    [SerializeField] private Transform _enemySpawnPoint;




    [Header ("Изменение параметров врага")]

    [Space (10)]

    // [SerializeField] private float moveSpeed = 3f;

    // [SerializeField] private float attackRange = 1f;

    [SerializeField] private int attackDamage = 10;

    private Transform player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private Animator animator;
    private NavMeshAgent AI_agent;
    private enum AI_state {Patrol, Stay};
    private int currentPoint;
    
    private float attackTimer;
    
    [Header ("Точки патрулирования врага")]

    [Space (10)]

    [SerializeField] private Transform[] WayPoints;

    [SerializeField] private AI_state AI_enemy;

    private void Start()
    {
        AI_agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(AI_enemy == AI_state.Patrol)
        {
            AI_agent.Resume();
            animator.SetBool("move", true);
            AI_agent.SetDestination(WayPoints[currentPoint].transform.position);
            float pointDistance = Vector3.Distance(WayPoints[currentPoint].transform.position, gameObject.transform.position);

            if(pointDistance < 4)
            {
                currentPoint++;
                currentPoint = currentPoint % WayPoints.Length;
            }
        }

        if(AI_enemy == AI_state.Stay)
        {
            animator.SetBool("move", false);
            AI_agent.Stop();
        }

        float playerDistance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if(playerDistance < 10)
        {
            attackTimer += Time.deltaTime;
        
            if(attackTimer >= 1)
            {
                Attack();
                Debug.Log("Противник выстрелил");
            }

            AI_agent.SetDestination(player.transform.position);
        }

        
    }

    private void Attack()
    {
        attackTimer = 0f;

        Rigidbody projectileInstance = Instantiate(_enemyAmmo, _enemySpawnPoint.position, _enemySpawnPoint.rotation);
        projectileInstance.velocity = projectileInstance.transform.forward * _shooting.AmmoSpeed;
        playerHealth.TakeDamage(attackDamage);
    }
}
