using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWall : MonoBehaviour
{
    private bool active = false;
    private string dir = null;

    private bool lowState = false;
    private bool moveState = false;
    private bool throwState = false;

    private Vector3 SpawnPointPos;
    private Quaternion SpawnPointRot;
    private float offset = 2;

    private GameObject Wall = null;
    private bool done = false;

    public void StartNow(GameObject wall, string parsedDir)
    {
        SpawnPointPos = transform.position;
        SpawnPointRot = transform.rotation;

        dir = parsedDir;

        if (dir == "Front")
        {
            SpawnPointPos = transform.position + (transform.forward * offset);
            SpawnPointRot = transform.rotation;
        }
        else if (dir == "Left")
        {
            SpawnPointPos = transform.position + (-transform.right * offset);
            SpawnPointRot = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0, 90, 0));
        }
        else
        {
            SpawnPointPos = transform.position + (transform.right * offset);
            SpawnPointRot = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 90, 0));
        }

        Wall = Instantiate(wall);
        Wall.transform.position = SpawnPointPos;
        wall.transform.rotation = SpawnPointRot;

        active = true;
    }

    private void FixedUpdate()
    {
        if (active && Wall != null)
        {
            if (lowState)
                LowerWall();
            else if (moveState)
                MoveWall();
            else if (throwState)
                ThrowWall();
        }

        if (done)
        {
            SendMessage("DoneCall");
        }
    }

    public void LowerWall()
    {
        if (!lowState)
        {

        }
        else
            lowState = true;
    }

    public void MoveWall()
    {
        if (!moveState)
        {

        }
        else
            moveState = true;
    }

    public void ThrowWall(string newDir = null)
    {
        switch (dir)
        {
            case "Front":
                switch (newDir)
                {
                    case "Front":
                        break;

                    case "Back":
                        break;

                    case "Left":
                        break;

                    case "Right":
                        break;
                }
                break;

            case "Left":
                switch (newDir)
                {
                    case "Front":
                        break;

                    case "Back":
                        break;

                    case "Left":
                        break;

                    case "Right":
                        break;
                }
                break;

            case "Right":
                switch (newDir)
                {
                    case "Front":
                        break;

                    case "Back":
                        break;

                    case "Left":
                        break;

                    case "Right":
                        break;
                }
                break;


        }
    }
}
