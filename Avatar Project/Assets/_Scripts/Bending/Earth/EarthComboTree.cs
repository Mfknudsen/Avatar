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

    private GameObject CurrentComboHolder = null;
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

                        for (int i = 0; i < 4; i++)
                        {
                            if (combo.requiredInputValues[i] != inputValues[i])
                                check = false;
                        }

                        if (combo.preIndex == preInputValues.Count)
                        {
                            for (int i = 0; i < combo.preIndex; i++)
                            {
                                bool[] toTest = new bool[4];

                                if (i == 0)
                                    toTest = combo.preValue1;
                                else if (i == 1)
                                    toTest = combo.preValue2;
                                else if (i == 2)
                                    toTest = combo.preValue3;
                                else if (i == 3)
                                    toTest = combo.preValue4;
                                else
                                    toTest = combo.preValue5;

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
        }
    }

    private void UseComboFunction(string comboFunction, string dir)
    {
        switch (comboFunction)
        {
            case "BoulderPunch":
                BoulderPunchFront(dir);
                break;

            case "MoveWall":
                MoveShield();
                break;

            case "EarthShield":
                EarthShield();
                break;
        }
    }

    #region ComboFunctions

    private void BoulderPunchFront(string dir)
    {
        Debug.Log("Boulder Punch: \n" + dir);


    }

    private void EarthShield()
    {
        Debug.Log("Earth Shield");

        GameObject obj = Instantiate(RockList["Rock Wall"]);

        obj.transform.rotation = transform.rotation;
        obj.transform.position = SpawnPoints[0].position - new Vector3(0, 1.5f, 0);

        RockWall script = obj.AddComponent<RockWall>();
        script.Wake();

        gameObject.GetComponent<PlayerMovement>().canMove = false;
    }

    private void MoveShield()
    {
        Debug.Log("Move Shield");
    }

    #endregion
}
