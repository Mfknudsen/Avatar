using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthComboTree : MonoBehaviour
{
    private ComboEarth[] EarthCombos = new ComboEarth[0];
    private ComboCollection Collection = null;

    private string direction = "";
    private bool[] inputValues = new bool[0];
    private List<string> preDirection = new List<string>();
    private List<bool[]> preInputValues = new List<bool[]>();

    private bool comboFound = false;
    private ComboEarth selectedCombo = null;
    public bool comboReady = true;

    public List<string> RockNames;
    public List<GameObject> RockGameObjects;
    private Dictionary<string, GameObject> RockList = new Dictionary<string, GameObject>();

    public Transform[] SpawnPoints = new Transform[4];

    private void Start()
    {
        if (Collection == null)
            Collection = GameObject.FindGameObjectWithTag("BendingSystem").GetComponent<ComboCollection>();

        if (EarthCombos.Length == 0)
            EarthCombos = Collection.EarthCombos.ToArray();

        for (int i = 0; i < RockNames.Count; i++)
            RockList.Add(RockNames[i], RockGameObjects[i]);

        Debug.Log(RockNames.Count);
        Debug.Log(RockGameObjects.Count);
    }

    public void GetInput(InputParameters ip)
    {
        direction = ip.Dir;
        inputValues = ip.Buttons.ToArray();

        CheckCombos();
    }

    private void CheckCombos()
    {
        selectedCombo = null;
        if (comboReady)
        {
            foreach (ComboEarth combo in EarthCombos)
            {
                if (!comboFound)
                {
                    if (combo.direction[combo.dirIndex] == direction)
                    {
                        bool check = true;
                        if (inputValues != combo.requiredInputValues)
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
        }
    }

    private void UseComboFunction(string comboFunction, string dir)
    {
        switch (comboFunction)
        {
            case "BoulderPunch":
                BoulderPunchFront(dir);
                break;

            case "":
                break;

            case "EarthShield":
                EarthShield();
                break;
        }
    }

    #region ComboFunctions

    private void BoulderPunchFront(string dir)
    {
        Debug.Log("Boulder Punch:  " + dir);


    }

    public void EarthShield()
    {
        Debug.Log("Earth Shield");
    }

    #endregion
}
