using UnityEngine;

/* Добавляем поля скорости перемещения, поворота, гравитации в классе контейнере
   Его мы будем наследовать от Scriptable Object
   Все поля будут приватными, а все данные будем получать с помощью свойств, бьез возмодности изменения.
   Таким образом мы сможем только считывать данные
*/

[CreateAssetMenu(fileName = "Characteristics", menuName = "Movement/MovementCharacteristics", order = 1)]

public class MovementCharacteristics : ScriptableObject
{
    [SerializeField] private bool _visibleCursor = false;

    [SerializeField] private float _movementSpeed = 1f;

    [SerializeField] private float _angularSpeed = 150f;
    
    [SerializeField] private float _gravity = 15f;

    [SerializeField] private float _jumpForce = 7f;


    public bool VisibleCursor => _visibleCursor;

    public float MovementSpeed => _movementSpeed;

    public float AngularSpeed => _angularSpeed;

    public float Gravity => _gravity;

    public float JumpForce => _jumpForce;
}
