using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    #region Public Data
    public GameObject Cam;
    public Transform Target, Player;
    #endregion

    #region Private Data
    private float rotSpeed = 1;
    private float mouseX, mouseY;
    #endregion

    void Start()
    {

    }

    void Update()
    {
        GetInput();

        Rotation();
    }

    private void GetInput()
    {
        mouseX += Input.GetAxis("Mouse X") * rotSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);
    }

    private void Rotation()
    {
        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
}
