using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EarthCombos : MonoBehaviour
{
    [Space]
    public Transform[] SpawnPoints;
    [Header("Rock List:")]
    public string[] rockNames;
    public GameObject[] rockPrefabs;
    private Dictionary<string, GameObject> rockList;
    [HideInInspector]
    public GameObject CurrentRocks;
    private string moveDirection = "Front";
    private Dictionary<string, bool> buttonInput;

    private void Start()
    {
        rockList = new Dictionary<string, GameObject>();
        for (int i = 0; i < rockNames.Length; i++)
            rockList.Add(rockNames[i], rockPrefabs[i]);
    }

    public void ActInput(InputParameters Input)
    {
        moveDirection = Input.Dir;
        buttonInput = new Dictionary<string, bool>();
        buttonInput = Input.Buttons;

        ComboControlTree();
    }

    private void ComboControlTree()
    {
        if (buttonInput["Fast"] && moveDirection == "Front")
        {
            GetRock1();
        }
        else if (buttonInput["Fast"] && moveDirection == "Right")
        {
            GetRock2();
        }
        else if (buttonInput["Fast"] && moveDirection == "Left")
        {
            GetRock3();
        }
    }

    #region ComboParts
    private void GetRock1()
    {
        /// Spawn and prepare a little rock in front of the player using PointFront.

        GameObject newRock = Instantiate(rockList["TinyRock"]);
        newRock.transform.position = SpawnPoints[0].position;
        newRock.transform.rotation = SpawnPoints[0].rotation;
    }

    private void GetRock2()
    {
        /// Spawn and prepare a little rock to the left of the player using PointFront.

        GameObject newRock = Instantiate(rockList["TinyRock"]);
        newRock.transform.position = SpawnPoints[2].position;
        newRock.transform.rotation = SpawnPoints[2].rotation;
    }

    private void GetRock3()
    {
        /// Spawn and prepare a little rock to the right of the player using PointFront.

        GameObject newRock = Instantiate(rockList["TinyRock"]);
        newRock.transform.position = SpawnPoints[3].position;
        newRock.transform.rotation = SpawnPoints[3].rotation;
    }

    private void AccelerateRock1()
    {

    }

    private void AccelerateRock2()
    {

    }

    private void AccelerateRock3()
    {

    }

    private void ThrowRock1()
    {

    }

    private void ThrowRock2()
    {

    }

    private void ThrowRock3()
    {

    }

    private void CatchRock1()
    {

    }

    private void CatchRock2()
    {

    }

    private void CatchRock3()
    {

    }

    private void BlockRock1()
    {

    }

    private void BlockRock2()
    {

    }

    private void BlockRock3()
    {

    }

    private void MoveRock1()
    {

    }

    private void MoveRock2()
    {

    }

    private void MoveRock3()
    {

    }
    #endregion
}
