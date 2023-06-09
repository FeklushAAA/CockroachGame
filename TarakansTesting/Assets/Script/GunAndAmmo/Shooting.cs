using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header ("Подключаемые объекты")]

    [Space (10)]

    [SerializeField] private GameObject target;
    
    [SerializeField] private CameraCharacteristics _camCharacteristics;

    [SerializeField] private ShootingCharacteristics _shooting; //Ссылка на значения характеристик из другого скрипта
    
    [SerializeField] private Rigidbody _ammo;

    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private Camera cam;
    

    private void Update()
    {
            if (Input.GetButtonDown("Fire1"))
            {
                Rigidbody projectileInstance = Instantiate(_ammo, _spawnPoint.position, _spawnPoint.rotation);
                projectileInstance.velocity = _spawnPoint.forward * _shooting.AmmoSpeed;
                projectileInstance.AddForce(_spawnPoint.forward * _shooting.AmmoForce);
                Debug.Log("Игрок выстрелил");
            }
        
    
        Vector3 aimSpot = cam.transform.position;
        //You will want to play around with the 50 to make it feel accurate.
        aimSpot += cam.transform.forward * 50.0f;
        aimSpot += cam.transform.right * 1.5f;
        transform.LookAt(aimSpot);
    }
}
