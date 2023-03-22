using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private ShootingCharacteristics _shooting; //Ссылка на значения характеристик из другого скрипта
    
    [SerializeField] private Rigidbody _ammo;

    [SerializeField] private Transform _spawnPoint;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody projectileInstance = Instantiate(_ammo, _spawnPoint.position, _spawnPoint.rotation);
            projectileInstance.velocity = _spawnPoint.forward * _shooting.AmmoSpeed;
            projectileInstance.AddForce(_spawnPoint.forward * _shooting.AmmoForce);
            // Add shooting sound effect here
            Debug.Log("Игрок выстрелил");
        }
    }
}
