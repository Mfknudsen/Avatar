using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOrbit : MonoBehaviour
{
    public int maxRockCount = 1;
    public List<GameObject> Rocks;
    public Transform[] StartEndPoints;

    void Start()
    {
        //Rocks = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Rocks.Count > 0)
            Rocks.Remove(Rocks[Rocks.Count - 1]);

        int divider = 0;
        if (Rocks.Count == 1)
            divider = 2;
        else if (Rocks.Count > 1)
            divider = 1 + Rocks.Count;

        if (divider != 0)
        {
            Vector3 dirVec = StartEndPoints[1].position - StartEndPoints[0].position;
            dirVec /= divider;

            for (int i = 0; i < Rocks.Count; i++)
            {
                Rocks[i].transform.position = Vector3.Lerp(Rocks[i].transform.position, StartEndPoints[0].position + dirVec * (i + 1), 0.35f);
                Rocks[i].transform.rotation = Quaternion.Euler(Vector3.Lerp(Rocks[i].transform.rotation.eulerAngles, StartEndPoints[0].rotation.eulerAngles, 0.35f));
            }
        }
    }
}
