using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform Cam;

    public float speed = 5.0f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public bool canMove = true; 

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (canMove) {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngel = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + Cam.eulerAngles.y;
                float angel = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, angel, 0.0f);

                Vector3 moveDir = Quaternion.Euler(0.0f, targetAngel, 0.0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
    }
}
