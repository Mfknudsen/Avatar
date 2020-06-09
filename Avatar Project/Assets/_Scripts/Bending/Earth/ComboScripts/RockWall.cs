using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWall : MonoBehaviour
{
    private float speed = 10.0f;
    private Vector3 startPos;
    private Vector3 endPos = new Vector3();
    private bool keepUp = true;
    private bool moveWall = false;

    public void Wake()
    {
        startPos = transform.position;

        endPos = startPos + new Vector3(0, 3, 0);
    }

    private void Update()
    {
        if (keepUp)
        {
            Rise();
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                moveWall = true;
                keepUp = false;
            }
        }
        else if (moveWall)
        {
            Move();
        }
        else
        {
            Fall();
        }

        if (!Input.GetKey(KeyCode.Space) && !moveWall)
            keepUp = false;
    }

    private void Rise()
    {
        if (transform.position.y < endPos.y)
            transform.position += transform.up * speed * Time.deltaTime;
        else
            transform.position = new Vector3(transform.position.x, endPos.y, transform.position.z);
    }

    private void Fall()
    {
        if (transform.position.y > startPos.y)
            transform.position += -transform.up * speed * Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (moveWall)
        {
            if (other.gameObject.tag == "Enemy")
            {
                moveWall = false;

                Destroy(gameObject);
            }
        }
    }
}
