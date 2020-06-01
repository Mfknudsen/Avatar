using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyRock : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    public float RotSpeed = 1.0f;
    public bool StartNow = false;

    void Update()
    {
        if (StartNow)
        {
            Move();
            Rotate();
        }
    }

    private void Move()
    {

    }

    private void Rotate()
    {

    }
}
