using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public Data
    public GameObject PlayerModel;
    public Transform CamTransform;
    public Transform CamHolder;
    public Transform TargetPos;
    public Transform temp;
    #endregion

    #region Private Data
    private PlayerCamera PlayerCam;
    private Vector3 MoveDir, RotDir;
    private float inputX, inputZ;
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private float rotSpeed = 1;
    private bool isGrounded = false;
    private bool jumpNow = false;
    #endregion

    private void Start()
    {
        PlayerCam = GetComponent<PlayerCamera>();
    }

    private void Update()
    {
        GetInput();

        Movement();
        Rotation();
        Jump();
    }

    private void FixedUpdate()
    {
        GroundDetect();
    }

    private void GetInput()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space) && !jumpNow && isGrounded)
            jumpNow = true;

    }

    private void Movement()
    {
        if (inputZ != 0)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + PlayerModel.transform.forward * inputZ * moveSpeed * Time.deltaTime, 0.5f);
        }
    }

    private void Rotation()
    {
        PlayerModel.transform.rotation = Quaternion.Euler(new Vector3(0,PlayerModel.transform.rotation.eulerAngles.y + inputX * rotSpeed * Time.deltaTime,0));
    }

    private void Jump()
    {
        if (jumpNow)
        {

            jumpNow = false;
        }
    }

    private void GroundDetect()
    {

    }
}
