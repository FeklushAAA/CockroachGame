using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private CameraCharacteristics _camCharacteristics;

    [SerializeField] private ShootingCharacteristics _shooting; //Ссылка на значения характеристик из другого скрипта
    
    [SerializeField] private Rigidbody _ammo;

    [SerializeField] private Transform _spawnPoint;

    [SerializeField] public float yDeg = 0.0f;

    [SerializeField] private float xDeg = 0.0f;


    private Quaternion _look; //Переменная для записи поворота персонажа
    
    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yDeg = angles.y;
        xDeg = angles.x;

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody projectileInstance = Instantiate(_ammo, _spawnPoint.position, _spawnPoint.rotation);
            projectileInstance.velocity = _spawnPoint.forward * _shooting.AmmoSpeed;
            projectileInstance.AddForce(_spawnPoint.forward * _shooting.AmmoForce);
            // Add shooting sound effect here
            Debug.Log("Игрок выстрелил");
        }
        xDeg += Input.GetAxis("Mouse X") * _camCharacteristics.xSpeed * 0.02f;
        yDeg = Mathf.Clamp(yDeg, _camCharacteristics.MinVerticalAngle, _camCharacteristics.MaxVerticalAngle); // ограничиваем угол возвышения камеры        
        yDeg -= Input.GetAxis("Mouse Y") * _camCharacteristics.ySpeed * 0.02f;   
        _spawnPoint.transform.rotation = Quaternion.Euler(yDeg, xDeg, 0);
    }
}
