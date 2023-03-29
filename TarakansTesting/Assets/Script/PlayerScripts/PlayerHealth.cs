using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header ("Количество ХП игрока")]

    [Space (10)]

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

    private void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        Debug.Log("Игрок умер");
    }
}
