using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private ShootingCharacteristics _shooting; //Ссылка на значения характеристик из другого скрипта
    public int startingHealth = 100;
    public int currentHealth;

    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;

    private void Awake()
    {
        hitParticles = GetComponentInChildren<ParticleSystem>();
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

        hitParticles.Play();

        if(currentHealth <= 0 )
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        Destroy(gameObject);
        Debug.Log("Umer");
    }
}
