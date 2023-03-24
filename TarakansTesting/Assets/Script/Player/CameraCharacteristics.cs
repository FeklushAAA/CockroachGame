using UnityEngine;

[CreateAssetMenu(fileName = "CameraCharacteristics", menuName = "Camera/CameraCharacteristics", order = 3)]


public class CameraCharacteristics : ScriptableObject
{
    [SerializeField] private float _maxVerticalAngle = 45.0f; 
    [SerializeField] private float _minVerticalAngle = -10.0f;

    [SerializeField] private float _targetHeight = 1.7f;
    [SerializeField] private float _distance = 5.0f;
    [SerializeField] private float _offsetFromWall = 0.1f;
    
    [SerializeField] private float _maxDistance = 20;
    [SerializeField] private float _minDistance = .6f;
    
    [SerializeField] private float _xSpeed = 200.0f;
    [SerializeField] private float _ySpeed = 200.0f;

    [SerializeField] private int _yMinLimit = -80;
    [SerializeField] private int _yMaxLimit = 80;

    [SerializeField] private int _zoomRate = 40;
    [SerializeField] private float _rotationDampening = 3.0f;
    [SerializeField] private float _zoomDampening = 5.0f;

    [SerializeField] private LayerMask _collisionLayers = -1;


    public float MaxVerticalAngle => _maxVerticalAngle;

    public float MinVerticalAngle => _minVerticalAngle;

    public float targetHeight => _targetHeight;

    public float distance => _distance;

    public float offsetFromWall => _offsetFromWall;
    
    public float maxDistance => _maxDistance;

    public float minDistance => _minDistance;
    
    public float xSpeed => _xSpeed;

    public float ySpeed => _ySpeed;
    
    public int yMinLimit => _yMinLimit;

    public int yMaxLimit => _yMaxLimit;
    
    public int zoomRate => _zoomRate;
    
    public float rotationDampening => _rotationDampening;

    public float zoomDampening => _zoomDampening;
    
    public LayerMask collisionLayers => _collisionLayers;
}