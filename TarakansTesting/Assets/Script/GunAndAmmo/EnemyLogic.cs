using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private ShootingCharacteristics _shooting; //Ссылка на значения характеристик из другого скрипта
    
    [SerializeField] private Rigidbody _enemyAmmo;

    [SerializeField] private Transform _enemySpawnPoint;


    public float moveSpeed = 3f;
    public float attackRange = 1f;
    public int attackDamage = 10;

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    Animator anim;

    float attackTimer;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        Move();

        attackTimer += Time.deltaTime;
        
        if(attackTimer >= 1)
        {
            Attack();
            Debug.Log("Противник выстрелил");
        }
    }


    void Move()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void Attack()
    {
        attackTimer = 0f;

        Rigidbody projectileInstance = Instantiate(_enemyAmmo, _enemySpawnPoint.position, _enemySpawnPoint.rotation);
        projectileInstance.velocity = projectileInstance.transform.forward * _shooting.AmmoSpeed;
        // projectileInstance.AddForce(_enemySpawnPoint.forward * _shooting.AmmoForce);
        playerHealth.TakeDamage(attackDamage);
    }
}
