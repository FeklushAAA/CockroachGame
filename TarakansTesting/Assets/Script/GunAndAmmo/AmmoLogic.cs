using UnityEngine;

public class AmmoLogic : MonoBehaviour
{
    [Header ("Подключаем Scriptable object")]

    [Space (10)]

    [SerializeField] private ShootingCharacteristics _shooting; //Ссылка на значения характеристик из другого скрипта

    [Header ("Жизнь пули после спавна")]

    [Space (10)]

    [SerializeField] private float _lifeTime;

    private Rigidbody rb;

    private void Start()
    {
        Invoke("DestroyBullet", _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(_shooting.Damage);
            DestroyBullet();
            Debug.Log("Противник получил урон");
        }

        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(_shooting.Damage);
            DestroyBullet();
            Debug.Log("Игрок получил урон");
        }
    }

    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
