#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Moves : MonoBehaviour
{
    #region Public Data
    public GameObject TinyRock, RockWall;
    public GameObject TestDummy;
    public GameObject Player;
    #endregion

    #region Private Data
    private List<GameObject> CurrentRocks = new List<GameObject>();
    bool ReadyForNextMove = true;
    Dictionary<string, bool> Inputs = new Dictionary<string, bool>();
    private int ComboCounter = 1;
    #endregion

    private void Update()
    {
        if (ReadyForNextMove)
        {
            if (Input.anyKeyDown)
            {
                StartCoroutine(WaitForExtraInput());
            }
        }

        Inputs["Key1"] = Input.GetKey(KeyCode.Alpha1);
        Inputs["Key2"] = Input.GetKey(KeyCode.Alpha2);
    }

    private IEnumerator WaitForExtraInput()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        if (CurrentRocks.Count == 0 && ReadyForNextMove)
        {
            if (Inputs["Key1"] && Inputs["Key2"])
                StartCoroutine(CreateRockWall());
            else if (Inputs["Key1"])
                StartCoroutine(LiftRock1());
        }
        else if (ReadyForNextMove)
        {
            if (Inputs["Key2"])
                StartCoroutine(PunchRock1());
        }
    }

    private IEnumerator LiftRock1()
    {
        Vector3 readyPos = Player.transform.position + new Vector3(0, 0.5f, 0) + Player.transform.forward * 1.5f;
        GameObject obj = Instantiate(TinyRock, Player.transform.position - new Vector3(0, 1, 0) + Player.transform.forward * 1.5f, Player.transform.rotation);
        CurrentRocks.Add(obj);
        obj.GetComponent<BoxCollider>().enabled = false;

        while (Vector3.Distance(obj.transform.position, readyPos) > 0.2f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, readyPos, 0.1f);
            yield return null;
        }

        ReadyForNextMove = true;
    }

    private IEnumerator PunchRock1()
    {
        Vector3 target = TestDummy.transform.position;
        GameObject obj = CurrentRocks[0];
        Vector3 startPos = obj.transform.position;
        float moveSpeed = 5f;

        float AnimTimer = 0;

        while (obj != null)
        {
            AnimTimer += Time.deltaTime * moveSpeed;
            AnimTimer = AnimTimer % 5f;

            obj.transform.position = MathParabola.Parabola(startPos, target, 5, AnimTimer / 5f, ComboCounter);
            //obj.transform.position = Vector3.Lerp(obj.transform.position, target, 0.1f);

            if (obj.GetComponent<BoxCollider>().bounds.Intersects(TestDummy.GetComponent<CapsuleCollider>().bounds))
            {
                ComboCounter *= -1;
                Destroy(obj);
                CurrentRocks.Remove(obj);
                obj = null;
            }

            yield return null;
        }
    }

    private IEnumerator CreateRockWall()
    {
        ReadyForNextMove = false;
        bool isWallUp = false;
        bool removeWall = false;
        GameObject obj = Instantiate(RockWall, Player.transform.position - new Vector3(0, 3f, 0) + Player.transform.forward * 2, Player.transform.rotation);
        Vector3 startPos = obj.transform.position;
        Vector3 endPos = obj.transform.position + obj.transform.up * 3.5f;

        while (obj != null)
        {
            if (!isWallUp)
            {
                if (Vector3.Distance(obj.transform.position, endPos) > 0.2f)
                    obj.transform.position = Vector3.Lerp(obj.transform.position, endPos, 0.05f);
                else
                    isWallUp = true;
            }
            else
            {
                if (!removeWall)
                {
                    if (!Input.GetKey(KeyCode.Alpha2))
                        removeWall = true;
                }
                else
                {
                    if (Vector3.Distance(obj.transform.position, startPos) > 0.2f)
                    {
                        obj.transform.position = Vector3.Lerp(obj.transform.position, startPos, 0.05f);
                    }
                    else
                    {
                        ReadyForNextMove = true;
                        Destroy(obj);
                    }
                }
            }

            yield return null;
        }
    }
}
