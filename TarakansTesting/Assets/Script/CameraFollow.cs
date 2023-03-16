using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] [Range(1f, 5f)] private float _angulatSpeedHorizontal = 1f; //Создали переменную для размера скорости

    [SerializeField] [Range(1f, 5f)] private float _angulatSpeedVertical = 1f; //Создали переменную для размера скорости по вертикали

    [SerializeField] [Range(30.0f, 45.0f)] private float maxVerticalAngle = 45.0f; // максимальный угол возвышения камеры

    [SerializeField] [Range(0.0f, -30.0f)] private float minVerticalAngle = -45.0f; // минимальный угол возвышения камеры

    [SerializeField] private Transform _target; //Добавляем сюда нашего персонажа, вокруг которого будет двигаться камера

    private float _angleHorizontal; // Переменная для трансформа по Х горизонтали

    private float _angleVertical; // Переменная для трансформа по Y Вертикали

    private void Start()
    {
        _angleVertical = transform.rotation.y;
        _angleHorizontal = transform.rotation.x;
        Screen.lockCursor = true;
    }

    private void Update()
    {
        _angleVertical = Mathf.Clamp(_angleVertical, minVerticalAngle, maxVerticalAngle); // ограничиваем угол возвышения камеры

        _angleVertical -= Input.GetAxis("Mouse Y") * _angulatSpeedVertical; //Умножаем скорость передвижения камеры на скорость изменения координаты мыши по вертикали
        _angleHorizontal += Input.GetAxis("Mouse X") * _angulatSpeedHorizontal; //Умножаем скорость передвижения камеры на скорость изменения координаты мыши по горизонтали

        transform.position = _target.transform.position;
        transform.rotation = Quaternion.Euler(_angleVertical, _angleHorizontal, 0); //Вычисляем кватернион поворота камеры

        if (Input.GetKeyDown("escape"))
            Screen.lockCursor = false;
    }




}
