using UnityEngine;

[RequireComponent(typeof(Animator))] //Автоматическое добавление компонента на объект
[RequireComponent(typeof(CharacterController))] //Автоматическое добавление компонента на объект

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera; //Поле для пустого объекта с камеры
    
    [SerializeField] private MovementCharacteristics _characteristics; //Ссылка на значения характеристик из другого скрипта

    [SerializeField] private CameraCharacteristics _camCharacteristics;


    private float _vertical, _horizontal; //Переменные для вертикали и горизонтали

    private readonly string STR_VERTICAL = "Vertical"; //Строчная переменная для обращения к компоненту Vertical в PlayerPrefs для отслеживания изменения координат

    private readonly string STR_HORIZONTAL = "Horizontal"; //Строчная переменная для обращения к компоненту Horizontal в PlayerPrefs для отслеживания изменения координат

    private readonly string STR_JUMP = "Jump"; // Строчная переменная для обращения к переменной в аниматоре прыжка

    private readonly string STR_AIR = "isInAir"; // Строчная переменная для обращения к переменной в аниматоре для проверки нахождения персонажа на земле


    private CharacterController _controller; //Компонент CharacterController,который будет двигать персонажа

    private Animator _animator; //Создаем переменноую аниматора для упрощения обращения 

    private Vector3 _direction; //Переменная для записи направления перемещения

    private Quaternion _look; //Переменная для записи поворота персонажа

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _controller = GetComponent<CharacterController>(); //Считываем Черектер контроллер

        Cursor.visible = _characteristics.VisibleCursor; //Отображение курсора зависит от флага в контейнере
        
    }

    private void Update()
    {

 

    }

    private void FixedUpdate()
    {
        Movement();
        Rotate();
        InAir();
    }

    private void InAir()
    {
        if(_controller.isGrounded) //Проверка стоит ли на земле наш игрок
        {
            _animator.SetBool(STR_AIR, false);//Прописываем проверку в воздухе ли мы для анимации падения, если true - анимация не проигрывается,
                                            //если false - анимация падения запускается после срабатывания анимации прыжка и длится до тех пор,
                                            //пока персонаж не приземлиться, затем срабатывает анимация приземления
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
        Vector3 target = _camera.forward * 4f; //Начальный наклон камеры при запуске игры
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

    public void PlayAnimation()
    {
        _animator.SetFloat(STR_VERTICAL, _vertical);
        _animator.SetFloat(STR_HORIZONTAL, _horizontal);
    }    
}

