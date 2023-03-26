
using UnityEngine;
using System.Collections;

public class WoWcamera : MonoBehaviour
{
    [Header ("Подключаем нашего центрального игрока и камеру")]

    [Space(10)]

    [SerializeField] private Transform target;

    [SerializeField] private CameraCharacteristics _camCharacteristics;

    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;

    private Animator _animator;

    private bool isLookingUp = false;
    private bool isLookingDown = false;

    private float xDeg = 0.0f;
    public float yDeg = 0.0f;
       
    private void Start ()
    {
        _animator = target.GetComponent<Animator>(); //Инициализируем аниматор
        Cursor.lockState = CursorLockMode.Locked; //Блокируем курсор по центру
        Vector3 angles = transform.eulerAngles; 
        xDeg = angles.x;
        yDeg = angles.y;
        
        currentDistance = _camCharacteristics.distance;
        desiredDistance = _camCharacteristics.distance;
        correctedDistance = _camCharacteristics.distance;
        
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true; // Блокируем поворот у компонента Rigidbody
    }

    private void Update ()
    {
        Vector3 vTargetOffset;
        
        // Проверяем, если наш персонаж не обнаружен на сцене, камера ничего делать не будет
        if (!target)
                return;

        yDeg = Mathf.Clamp(yDeg, _camCharacteristics.MinVerticalAngle, _camCharacteristics.MaxVerticalAngle); // ограничиваем угол возвышения камеры        
        
        //При движении мыши будет изменяться переменная координаты, которая будет умножаться на скорость камеры
        xDeg += Input.GetAxis("Mouse X") * _camCharacteristics.xSpeed * 0.02f; 
        yDeg -= Input.GetAxis("Mouse Y") * _camCharacteristics.ySpeed * 0.02f;

        // Передаем в аниматор изменение координаты по У
        _animator.SetFloat("WeaponUp", yDeg);
           
        // Через кватернион подключаем поворот камеры
        Quaternion rotation = Quaternion.Euler (yDeg, xDeg, 0);
        
        // Логика приближения камеры при помощи колесика мыши
        desiredDistance -= Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * _camCharacteristics.zoomRate * Mathf.Abs (desiredDistance);
        desiredDistance = Mathf.Clamp (desiredDistance, _camCharacteristics.minDistance, _camCharacteristics.maxDistance);
        correctedDistance = desiredDistance;
        
        // Расчёт нужного нам расстояния
        vTargetOffset = new Vector3 (0, -_camCharacteristics.targetHeight, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + vTargetOffset);
        
        // Используя лучи проверяем соприкосновения с физическими объектами
        RaycastHit collisionHit;
        Vector3 trueTargetPosition = new Vector3 (target.position.x, target.position.y + _camCharacteristics.targetHeight, target.position.z);
        
        // Если камера сталкивается с физическим объектом, дистанция от камеры до игрока корректируется
        bool isCorrected = false;
        if (Physics.Linecast (trueTargetPosition, position, out collisionHit, _camCharacteristics.collisionLayers.value))
        {
            correctedDistance = Vector3.Distance (trueTargetPosition, collisionHit.point) - _camCharacteristics.offsetFromWall;
            isCorrected = true;
        }
           
        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp (currentDistance, correctedDistance, Time.deltaTime * _camCharacteristics.zoomDampening) : correctedDistance;
        
        currentDistance = Mathf.Clamp (currentDistance, _camCharacteristics.minDistance, _camCharacteristics.maxDistance);
        
        position = target.position - (rotation * Vector3.forward * currentDistance + vTargetOffset);
        
        transform.rotation = rotation;
        transform.position = position;
    }

}
 