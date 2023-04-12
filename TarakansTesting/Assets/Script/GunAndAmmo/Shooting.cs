using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header ("Подключаемые объекты")]

    [Space (10)]
    
    [SerializeField] private CameraCharacteristics _camCharacteristics;

    [SerializeField] private ShootingCharacteristics _shooting; //Ссылка на значения характеристик из другого скрипта
    
    [SerializeField] private Rigidbody _ammo;

    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private Camera cam;


    

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        RayToScreenCenter();
    }

    private void Fire()
    {
        Rigidbody projectileInstance = Instantiate(_ammo, _spawnPoint.position, _spawnPoint.rotation);
        projectileInstance.velocity = _spawnPoint.forward * _shooting.AmmoSpeed;
        projectileInstance.AddForce(_spawnPoint.forward * _shooting.AmmoForce);
        Debug.Log("Игрок выстрелил");
    }

    private void RayToScreenCenter()
    {
        Vector3 aimSpot = cam.transform.position;
        //You will want to play around with the 50 to make it feel accurate.
        aimSpot += cam.transform.forward * 100f;
        transform.LookAt(aimSpot);
    }
}
