using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboAir : ScriptableObject
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

    public void newPrevious(ref ComboCollection col)
    {
        preDirIdx.Add(0);
        preDirection.Add(col.Directions);
        previousInputKeys.Add(col.InputKey);
        previousInputValues.Add(col.InputValue);
    }

    public void removePrevious(int I)
    {
        int i = preDirIdx.Count - I;

        if (preDirIdx.Count > 0 && i < preDirIdx.Count)
            preDirIdx.RemoveAt(i);

        if (preDirection.Count > 0 && i < preDirection.Count)
            preDirection.RemoveAt(i);

        if (previousInputKeys.Count > 0 && i < previousInputKeys.Count)
            previousInputKeys.RemoveAt(i);

        if (previousInputValues.Count > 0 && i < previousInputValues.Count)
            previousInputValues.RemoveAt(i);
    }
}
