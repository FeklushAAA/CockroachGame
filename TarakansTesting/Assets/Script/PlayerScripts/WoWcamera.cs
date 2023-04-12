using UnityEngine.UI;
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

    [Header ("UI элементы в камере")]

    [Space(10)]

    [SerializeField] private GameObject playerUI;

    [SerializeField] private GameObject questsList;

    private Animator _animator;
    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
       
    private void Start ()
    {
        _animator = target.GetComponent<Animator>(); //Инициализируем аниматор
        Cursor.lockState = CursorLockMode.Locked; //Блокируем курсор по центру

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true; // Блокируем поворот у компонента Rigidbody
    }

    private void Update()
    {
        OnTabEnter();
    }

    private void OnTabEnter()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            playerUI.SetActive(!playerUI.activeSelf);
            questsList.SetActive(!questsList.activeSelf);
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
            pos = pos + (transform.rotation * Vector3.right * offsetPosition); //Сдвиг позиции вправо или влево
			position = new Vector3(pos.x, position.y, pos.z); // сдвиг позиции в точку контакта
		}
		return position;
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
    

}
 