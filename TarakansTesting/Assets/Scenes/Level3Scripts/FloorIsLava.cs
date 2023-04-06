using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorIsLava : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(100);
            Debug.Log("Противник получил урон");
        }

        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(100);
            Debug.Log("Игрок получил урон");
        }
    }
}
