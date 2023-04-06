using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header ("Количество ХП игрока")]

    [Space (10)]

    [SerializeField] private int startingHealth = 100;

    [SerializeField] private GameObject ragdoll;

    [SerializeField] private Image bar;

    private float fill = 1f;

    private float currentHealth;

    private CapsuleCollider capsuleCollider;

    private bool isDead;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    private void Update()
    {
        bar.fillAmount = fill;
        fill = currentHealth / 100;
    }

    public void TakeDamage(float amount)
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
        bar.fillAmount = 0.0f;
        isDead = true;
        capsuleCollider.isTrigger = true;
        Debug.Log("Игрок умер");
        gameObject.SetActive(false);
        ragdoll.SetActive(true);
        Instantiate(ragdoll, transform.position, transform.rotation);
    }
}
