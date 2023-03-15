using UnityEngine;

[RequireComponent(typeof(Animator))] //Автоматическое добавление компонента на объект
[RequireComponent(typeof(CharacterController))] //Автоматическое добавление компонента на объект

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera; //Поле для камеры
    
    [SerializeField] private MovementCharacteristics _characteristics; //Ссылка на значения характеристик из другого скрипта

    private float _vertical, _horizontal; //Переменные для вертикали и горизонтали

    private readonly string STR_VERTICAL = "Vertical";

    private readonly string STR_HORIZONTAL = "Horizontal";

    private readonly string STR_JUMP = "Jump";

    private readonly string STR_AIR = "isInAir";


    private CharacterController _controller; //Компонент CharacterController,который будет двигать персонажа

    private Animator _animator;

    private Rigidbody _rb;

    private const float DISTANSE_OFFSET_CAMERA = 5f; //Расстояние от камеры до точки, в которую будем поворачивать персонажа

    private Vector3 _direction; //Переменная для записи направления перемещения

    private Quaternion _look; //Переменная для записи поворота персонажа

    private Vector3 TargetRotate => _camera.forward * DISTANSE_OFFSET_CAMERA;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _rb = GetComponent<Rigidbody>();

        _controller = GetComponent<CharacterController>(); //Считываем Черектер контроллер

        Cursor.visible = _characteristics.VisibleCursor; //Отображение курсора зависит от флага в контейнере
        
    }

    private void FixedUpdate()
    {
        Movement();
        Rotate();
        InAir();
    }

    private void InAir()
    {
        if(_controller.isGrounded)
        {
            _animator.SetBool(STR_AIR, false);//Прописываем проверку в воздухе ли мы для анимации прыжка
        }
        else
        {
            _animator.SetBool(STR_AIR, true);
        }
    }

    private void Movement()
    {
        //Проверяем, если контроллер на земле, тогда записываем значения нажатых клавиш
        if(_controller.isGrounded)
        {
            _horizontal = Input.GetAxis(STR_HORIZONTAL);
            _vertical = Input.GetAxis(STR_VERTICAL);

            _direction = transform.TransformDirection(_horizontal, 0, _vertical).normalized; //Вычисляем новое направление движения в зависимости от клавиш

            PlayAnimation();
            Jump();
        }

        
        _direction.y -= _characteristics.Gravity * Time.deltaTime;//Отнимаем значение гравитации для притягивания персонажа к игровой поверхности
        Vector3 dir = _direction * _characteristics.MovementSpeed * Time.deltaTime; //Умножаем наше направление на стандартную скорость передвижения

        _controller.Move(dir); //Перемещаем наш контроллер
    }

    private void Rotate()
    {
        Vector3 target = TargetRotate;
        target.y = 0;

        _look = Quaternion.LookRotation(target);

        float speed = _characteristics.AngularSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, _look, speed);
    }

    private void Jump()
    {
        if(Input.GetButtonDown(STR_JUMP))
        {
            if(_controller.isGrounded)
            {
                _animator.SetTrigger(STR_JUMP);
                _direction.y += _characteristics.JumpForce;
            }
        }
        
    }

    private void PlayAnimation()
    {
        _animator.SetFloat(STR_VERTICAL, _vertical);
        _animator.SetFloat(STR_HORIZONTAL, _horizontal);
    }    
}
