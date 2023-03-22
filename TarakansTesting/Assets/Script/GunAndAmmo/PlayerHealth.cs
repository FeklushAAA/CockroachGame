using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;

    public int currentHealth;

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
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 10f, 0);
        Debug.Log("Игрок умер");
    }
}
