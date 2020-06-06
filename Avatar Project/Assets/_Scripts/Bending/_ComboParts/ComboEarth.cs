using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboEarth : ScriptableObject
{
    public string name = "New Air Combo";

    public string[] requiredInputKeys;
    public bool[] requiredInputValues;

    public int dirIndex = 0;
    public string[] direction;

    public int actIndex = 0;
    public string[] action;
    public string[] actionDescription;

    public int preIndex;
    public List<string[]> previousInputKeys;
    public List<bool[]> previousInputValues;
    public List<int> preDirIdx;
    public List<string[]> preDirection;

    public string getDir()
    {
        return direction[dirIndex];
    }

    public string getAct()
    {
        return action[actIndex];
    }
}