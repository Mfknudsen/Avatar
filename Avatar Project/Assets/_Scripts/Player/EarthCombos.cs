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
    [HideInInspector]
    public GameObject CurrentRocks;

    private Dictionary<string, GameObject> rockList;
    private string moveDirection = "Front";
    private Dictionary<string, bool> buttonInput;
    private string currentState = "readyState";

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
        switch (currentState)
        {
            case "readyState":
                if (buttonInput["Fast"])
                {
                    GetRock(moveDirection);
                    currentState = "hasRock";
                }

                break;

            case "hasRock":

                break;

            case "steadingSelf":

                break;

            case "stuned":

                break;

            default:
                Debug.Log("Default State Selected!");
                break;
        }
    }

    #region ComboParts
    private void GetRock(string direction)
    {
        /// Spawn and prepare a little rock using direction to select a SpawnPoint.

        int selectedSpawnPoint = 0;
        if (direction == "Front")
            selectedSpawnPoint = 0;

        GameObject newRock = Instantiate(rockList["TinyRock"]);
        newRock.transform.position = SpawnPoints[selectedSpawnPoint].position;
        newRock.transform.rotation = SpawnPoints[selectedSpawnPoint].rotation;
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
