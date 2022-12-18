using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickControl : MonoBehaviour
{   
    [Range(1, 10)]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Camera _camera;
     
    private JoystickControl _joystickControl;
    private Rigidbody _characterRigidbody;

    private Ray _ray;
    private RaycastHit _raycastHit;

    private bool _isClicked = false;

    private void Awake()
    {
        _characterRigidbody = GetComponent<Rigidbody>();
        _joystickControl = GetComponent<JoystickControl>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Movement();
        }
    }

    private void Movement()
    {
        if (_joystickControl.isActive)
            return;

        _ray = _camera.ScreenPointToRay(Input.GetTouch(Input.touchCount - 1).position);

        if (Physics.Raycast(_ray, out RaycastHit raycastHit))
        {
            this._raycastHit = raycastHit;
  
            if (!_isClicked)
                StartCoroutine(MovementCor());
        }

    }

    IEnumerator MovementCor()
    {
        _isClicked = true;

        while (Mathf.Round(transform.position.x) != Mathf.Round(_raycastHit.point.x))
        {
            if (_joystickControl.isActive)
                break;

            _characterRigidbody.velocity = Vector3.MoveTowards(_characterRigidbody.velocity, new Vector3(_raycastHit.point.x, _characterRigidbody.velocity.y, _raycastHit.point.z), _moveSpeed);
            yield return new WaitForSeconds(0.1f);  
            
        }

        _isClicked = false;
    }
}
