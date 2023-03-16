using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootingCharacteristics", menuName = "Shooting/ShootingCharacteristics", order = 2)]

public class ShootingCharacteristics : ScriptableObject
{
    [SerializeField] private float _ammoSpeed;

    [SerializeField] private float _ammoForce;

    // [SerializeField] public float _lifeTime;

    public float AmmoForce => _ammoForce;
    
    public float AmmoSpeed => _ammoSpeed;

    // public float LifeTime => _lifeTime;
}
