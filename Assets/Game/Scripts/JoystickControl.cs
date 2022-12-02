using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JoystickControl : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [Range(1,10)]
    [SerializeField] private float moveSpeed;

    private Rigidbody characterRigidbody;
    public bool isActive;


    private void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            isActive = true;
            Movement();
            
        }else
            isActive = false;


    }

    private void Movement()
    {
        //Движение по оси Z
        characterRigidbody.velocity = new Vector3(joystick.Horizontal * moveSpeed, characterRigidbody.velocity.y, characterRigidbody.velocity.z);

        //Движение по оси ZX
        // characterRigidbody.velocity = new Vector3(joystick.Vertical * moveSpeed, characterRigidbody.velocity.y, joystick.Horizontal * moveSpeed);
    }
}
