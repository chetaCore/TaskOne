using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickControl : MonoBehaviour
{   
    [Range(1, 10)]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera _camera;
    
    
    private JoystickControl joystickControl;
    private Rigidbody characterRigidbody;

    private Ray ray;
    RaycastHit raycastHit;


    private bool isClicked = false;

    private void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        joystickControl = GetComponent<JoystickControl>();
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
        if (joystickControl.isActive)
            return;


        ray = _camera.ScreenPointToRay(Input.GetTouch(Input.touchCount - 1).position);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            this.raycastHit = raycastHit;
  
            if (!isClicked)
                StartCoroutine(MovementCor());
        }
        
        
    }

    IEnumerator MovementCor()
    {
        isClicked = true;

        while (Mathf.Round(transform.position.x) != Mathf.Round(raycastHit.point.x))
        {
            if (joystickControl.isActive)
                break;

            characterRigidbody.velocity = Vector2.MoveTowards(characterRigidbody.velocity, new Vector2(raycastHit.point.x, characterRigidbody.velocity.y), moveSpeed);
            yield return new WaitForSeconds(0.1f);  
            
        }

        isClicked = false;
    }
}
