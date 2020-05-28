using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    public GameObject EnemyTarget;
    public CinemachineFreeLook CamController;
    public Transform CamFocusPoint;

    bool locked = false;
    Vector3 start = Vector3.zero;

    private void Start()
    {
        start = CamFocusPoint.localPosition;
    }

    private void Update()
    {
        if (locked)
        {
            CamFocusPoint.position = EnemyTarget.transform.position - transform.position;
        }
        else
        {
            CamFocusPoint.localPosition = start;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (locked)
                locked = false;
            else
                locked = true;
        }
    }
}
