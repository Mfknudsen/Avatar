using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EarthCombos : MonoBehaviour
{
    [Space]
    public Transform[] SpawnPoints;
    [Header("Rock List:")]
    public string[] rockNames;
    public GameObject[] rockPrefabs;
    private Dictionary<string, GameObject> rockList;
    [Header("Combo List")]
    public ComboEarth[] comboList = new ComboEarth[] { };
    [HideInInspector]
    public GameObject CurrentRocks;
    private string moveDirection = "Front";
    private Dictionary<string, bool> buttonInput;
    private List<Dictionary<string, bool>> lastInput;

    private void Start()
    {
        rockList = new Dictionary<string, GameObject>();
        if (rockNames.Length == rockPrefabs.Length)
        {
            for (int i = 0; i < rockNames.Length; i++)
                rockList.Add(rockNames[i], rockPrefabs[i]);
        }
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
        foreach (ComboEarth comboPart in comboList)
        {
            bool checkReq = true;
            Dictionary<string, bool> comboRequiredInput = comboPart.getReq();

            foreach (string key in buttonInput.Keys)
            {
                if (buttonInput[key] != comboRequiredInput[key])
                {
                    checkReq = false;
                    break;
                }
            }

            if (moveDirection != comboPart.getDir())
                checkReq = false;

            bool checkPre = true;
            if (checkReq)
            {
                List<Dictionary<string, bool>> comboPreviousInput = comboPart.getPre();

                if (lastInput.Count == comboPreviousInput.Count)
                {
                    for (int i = 0; i < lastInput.Count; i++)
                    {
                        Dictionary<string, bool> currentLastInput = lastInput[i];
                        Dictionary<string, bool> currentComboPreviousInput = comboPreviousInput[i];

                        bool keepChecking = true;

                        while (keepChecking)
                        {
                            foreach (string key in currentLastInput.Keys)
                            {
                                if (currentLastInput[key] != currentComboPreviousInput[key])
                                {
                                    checkPre = false;
                                    keepChecking = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    checkPre = false;
                }
            }

            if (checkReq && checkPre)
            {
                string actionCommand = comboPart.getAct();

                if (actionCommand == "GetRock")
                    GetRock(buttonInput, moveDirection);

                lastInput.Add(buttonInput);
                buttonInput.Clear();
                break;
            }
            else
            {
                ResetComboTree();
            }
        }
    }

    private void ResetComboTree()
    {
        buttonInput.Clear();
        lastInput.Clear();
    }

    #region ComboParts
    #region ReadyRock
    private void GetRock(Dictionary<string, bool> input, string dir)
    {
        /// Spawn and prepare a little rock in front of the player using PointFront.
        int i = SpawnPoints.Length;
        if (dir == "Front")
            i = 0;
        else if (dir == "Left")
            i = 1;
        else if (dir == "Right")
            i = 2;

        if (i < SpawnPoints.Length && i > -1)
        {
            GameObject newRock = Instantiate(rockList["TinyRock"]);
            newRock.transform.position = SpawnPoints[i].position;
            newRock.transform.rotation = SpawnPoints[i].rotation;

            newRock.GetComponent<TinyRock>().StartNow = true;
        }
    }
    #endregion

    #region SpeedUpRock
    private void AccelerateRock1()
    {

    }

    private void AccelerateRock2()
    {

    }

    private void AccelerateRock3()
    {

    }
    #endregion

    #region ThrowRock
    private void ThrowRock1()
    {

    }

    private void ThrowRock2()
    {

    }

    private void ThrowRock3()
    {

    }
    #endregion

    #region CatchRock
    private void CatchRock1()
    {

    }

    private void CatchRock2()
    {

    }

    private void CatchRock3()
    {

    }
    #endregion

    #region Block
    private void BlockRock1()
    {

    }

    private void BlockRock2()
    {

    }

    private void BlockRock3()
    {

    }
    #endregion

    #region Move
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
    #endregion
}