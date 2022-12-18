using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JoystickControl : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [Range(1, 10)]
    [SerializeField] private float _moveSpeed;

    private Rigidbody _characterRigidbody;
    [HideInInspector] public bool isActive;


    private void Awake()
    {
        _characterRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_joystick.Vertical != 0 || _joystick.Horizontal != 0)
        {
            isActive = true;
            Movement();            
        }else
            isActive = false;
    }

    private void Movement()
    {
        _characterRigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _characterRigidbody.velocity.y, _joystick.Vertical * _moveSpeed);
    }
}
