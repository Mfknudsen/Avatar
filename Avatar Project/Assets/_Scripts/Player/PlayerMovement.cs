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
    public Transform MoveTransform;
    #endregion

    #region Private Data
    private Vector3 MoveDir, RotDir;
    private float inputX, inputZ;
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private float rotSpeed = 1;
    private bool isGrounded = false;
    private bool jumpNow = false;
    #endregion

    private void Update()
    {
        GetInput();

        if (true)
        {
            Movement();
            Rotation();
        }
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
        MoveTransform.LookAt(TargetPos);

        if (inputZ != 0 || inputX != 0)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + MoveTransform.forward * inputZ * moveSpeed * Time.deltaTime + MoveTransform.right * inputX * moveSpeed * Time.deltaTime, 0.5f);
        }
    }

    private void Rotation()
    {
        PlayerModel.transform.rotation = Quaternion.Euler(new Vector3(0, PlayerModel.transform.rotation.eulerAngles.y + inputX * rotSpeed * Time.deltaTime, 0));
    }
}
