using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header ("Подключаемые объекты")]

    [Space (10)]
    
    [SerializeField] private CameraCharacteristics _camCharacteristics;

    [SerializeField] private ShootingCharacteristics _shooting; //Ссылка на значения характеристик из другого скрипта
    
    [SerializeField] private Rigidbody _ammo;

    [SerializeField] private Transform _spawnPoint;

    [Header ("Начальные координаты по Х и У для спавнера пуль")]

    [Space (10)]

    [SerializeField] public float yDeg = 0.0f;

    [SerializeField] private float xDeg = 0.0f;

    private Vector3 pos;


    private Quaternion _look; //Переменная для записи поворота персонажа
    
    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yDeg = angles.y;
        xDeg = angles.x;
        pos = _spawnPoint.transform.position;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody projectileInstance = Instantiate(_ammo, _spawnPoint.position, _spawnPoint.rotation);
            projectileInstance.velocity = _spawnPoint.forward * _shooting.AmmoSpeed;
            projectileInstance.AddForce(_spawnPoint.forward * _shooting.AmmoForce);
            Debug.Log("Игрок выстрелил");
        }

        xDeg += Input.GetAxis("Mouse X") * _camCharacteristics.xSpeed * 0.02f;
        yDeg = Mathf.Clamp(yDeg, -50, 70); // ограничиваем угол возвышения камеры        
        yDeg -= Input.GetAxis("Mouse Y") * _camCharacteristics.ySpeed * 0.02f;   
        _spawnPoint.transform.rotation = Quaternion.Euler(yDeg, xDeg, 0);
    }
}
