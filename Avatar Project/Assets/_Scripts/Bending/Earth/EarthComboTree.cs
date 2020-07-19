using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthComboTree : MonoBehaviour
{
    private bool attempted = false;

    private ComboEarth[] EarthCombos = new ComboEarth[0];
    private ComboCollection Collection = null;

    private string direction = "";
    private bool[] inputValues = new bool[0];
    private List<string> preDirection = new List<string>();
    private List<bool[]> preInputValues = new List<bool[]>();

    private ComboEarth selectedCombo = null;
    public bool comboReady = true;

    public List<string> RockNames;
    public List<GameObject> RockGameObjects;
    private Dictionary<string, GameObject> RockList = new Dictionary<string, GameObject>();

    public GameObject ComboHolder;

    private void Start()
    {
        if (ComboHolder == null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == "ComboHolder")
                {
                    ComboHolder = transform.GetChild(i).gameObject;
                    break;
                }
            }
        }

        if (Collection == null)
            Collection = GameObject.FindGameObjectWithTag("BendingSystem").GetComponent<ComboCollection>();

        if (EarthCombos.Length == 0)
            EarthCombos = Collection.EarthCombos.ToArray();

        for (int i = 0; i < RockNames.Count; i++)
            RockList.Add(RockNames[i], RockGameObjects[i]);
    }

    public void GetInput(InputParameters ip)
    {
        direction = ip.Dir;
        inputValues = ip.Buttons.ToArray();

        CheckCombos();
    }

    public void DoneCall()
    {
        selectedCombo = null;

        comboReady = true;
    }

    private void CheckCombos()
    {
        selectedCombo = null;
        bool comboFound = false;

        if (comboReady)
        {
            foreach (ComboEarth combo in EarthCombos)
            {
                if (!comboFound)
                {
                    if (combo.direction[combo.dirIndex] == direction)
                    {
                        bool check = true;

                        if (combo.preIndex == preInputValues.Count)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (combo.requiredInputValues[i] != inputValues[i])
                                    check = false;
                            }

                            for (int i = 0; i < combo.preIndex; i++)
                            {
                                bool[] toTest = new bool[4];

                                for (int j = 0; j < 4; j++)
                                {
                                    if (toTest[j] != preInputValues[i][j])
                                        check = false;
                                }
                            }
                        }
                        else
                            check = false;

                        if (check)
                        {
                            selectedCombo = combo;
                            comboFound = true;
                        }
                    }
                }
            }

            if (selectedCombo != null)
            {
                UseComboFunction(selectedCombo.action[selectedCombo.actIndex], selectedCombo.direction[selectedCombo.dirIndex]);

                preDirection.Add(direction);
                preInputValues.Add(inputValues);
            }
            else
            {
                preDirection = new List<string>();
                preInputValues = new List<bool[]>();
            }

            direction = "";
            inputValues = new bool[0];

            if (preDirection.Count > 5)
            {
                preDirection = new List<string>();
                preInputValues = new List<bool[]>();
            }

            selectedCombo = null;

            if (!attempted)
            {
                attempted = true;
                CheckCombos();
            }
            else
                attempted = false;
        }
    }

    private void UseComboFunction(string comboFunction, string dir)
    {
        switch (comboFunction)
        {
            case "BoulderPunch":
                break;

            case "MoveWall":
                ComboHolder.GetComponent<RockWall>().MoveWall();
                break;

            case "EarthShield":
                ComboHolder.AddComponent<RockWall>().StartNow(RockList["Rock Wall"], dir);
                break;

            case "LowerWall":
                ComboHolder.GetComponent<RockWall>().LowerWall();
                break;

            case "ThrowWall":
                ComboHolder.GetComponent<RockWall>().ThrowWall(dir);
                break;

            case "RaiseSmallBoulder":
                ComboHolder.AddComponent<RaiseSmallBoulder>().RaiseBoulders(dir);
                break;

            case "KickSmallBoulder":
                break;

            case "ChargeLow":
                break;

            case "ChargeHigh":
                break;
        }
    }
}
