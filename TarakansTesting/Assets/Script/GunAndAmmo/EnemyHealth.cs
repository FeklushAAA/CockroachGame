using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;

    private int currentHealth;

    private CapsuleCollider capsuleCollider;

    private bool isDead;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        if(isDead)
        {
            return;
        }

        currentHealth -= amount;

        if(currentHealth <= 0 )
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        Destroy(gameObject);
        Debug.Log("EnemyUmer");
    }
}
