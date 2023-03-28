
using UnityEngine;
using System.Collections;

public class WoWcamera : MonoBehaviour
{
    [Header ("Подключаем нашего центрального игрока и камеру")]

    [Space(10)]

    [SerializeField] private Transform target;

    [SerializeField] private CameraCharacteristics _camCharacteristics;

    [Header("Смещение в бок")]

    [Space(10)]

	[SerializeField] private float offsetPosition; // смешение камеры вправо или влево, 0 = центр

    [Header("Основные настройки")]

    [Space(10)]

    [SerializeField] private float distance = 1f;

    [SerializeField] private float height = 2.3f;

    [SerializeField] private enum Smooth {Disabled = 0, Enabled = 1};
	[SerializeField] private Smooth smooth = Smooth.Enabled;
	[SerializeField] private float speed = 8; // скорость сглаживания

    // private float desiredDistance;
    // private float correctedDistance;

    private Animator _animator;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
       
    private void Start ()
    {
        _animator = target.GetComponent<Animator>(); //Инициализируем аниматор
        Cursor.lockState = CursorLockMode.Locked; //Блокируем курсор по центру

        // desiredDistance = _camCharacteristics.distance;
        // correctedDistance = _camCharacteristics.distance;
        
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true; // Блокируем поворот у компонента Rigidbody
    }

    private void LateUpdate ()
    {
        // Проверяем, если наш персонаж не обнаружен на сцене, камера ничего делать не будет
        if (!target)
                return;

        
        xDeg += Input.GetAxis("Mouse X") * _camCharacteristics.xSpeed * 0.02f;
        yDeg -= Input.GetAxis("Mouse Y") * _camCharacteristics.ySpeed * 0.02f;
        yDeg = Mathf.Clamp(yDeg, _camCharacteristics.MinVerticalAngle, _camCharacteristics.MaxVerticalAngle); // ограничиваем угол возвышения камеры        
        

        // Передаем в аниматор изменение координаты по У
        _animator.SetFloat("WeaponUp", yDeg);
           
        // Через кватернион подключаем поворот камеры
        Quaternion rotation = Quaternion.Euler(yDeg, xDeg, 0);
        
        // Логика приближения камеры при помощи колесика мыши
        // desiredDistance -= Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * _camCharacteristics.zoomRate * Mathf.Abs (desiredDistance);
        // desiredDistance = Mathf.Clamp (desiredDistance, _camCharacteristics.minDistance, _camCharacteristics.maxDistance);
        // correctedDistance = desiredDistance;

        Vector3 position = target.position - (rotation * Vector3.forward * distance);
        position = position + (rotation * Vector3.right * offsetPosition); // корректировка горизонтали
        position = new Vector3(position.x, target.position.y + height, position.z); // корректировка высоты
        position = PositionCorrection(target.position, position);

        transform.rotation = rotation;
        if(smooth == Smooth.Disabled) 
        {
            transform.position = position;
        }
		else 
        {
            transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
        }
        
    }
    Vector3 PositionCorrection(Vector3 Qtarget, Vector3 position)
	{
		RaycastHit hit;
		Debug.DrawLine(Qtarget, position, Color.blue);
		if(Physics.Linecast(Qtarget, position, out hit)) 
		{
			float tempDistance = Vector3.Distance(Qtarget, hit.point);
			Vector3 pos = Qtarget - (transform.rotation * Vector3.forward * tempDistance);
			position = new Vector3(pos.x, position.y, pos.z); // сдвиг позиции в точку контакта
		}
		return position;
	}

}
 