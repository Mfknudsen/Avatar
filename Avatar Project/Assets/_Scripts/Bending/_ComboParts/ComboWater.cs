using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboWater : ScriptableObject
{
    public string name = "New Water Combo";

    public string[] requiredInputKeys = new string[] {
        "Fast",
        "Heavy",
        "Special",
        "Block"
    };
    public bool[] requiredInputValues = new bool[4];

    public int dirIndex = 0;
    public string[] direction = new string[] {
        "Front",
        "Back",
        "Left",
        "Right"
    };

    public int actIndex = 0;
    public string[] action = new string[] {
        "WaterWhip",
        "IceSpike"
    };
    public string[] actionDescription = new string[] {
        "Whip the opponent with a whip of water.",
        "Send an ice spike towards the opponents position."
    };

    public int preIndex = 0;
    #region Previous Input Values
    public bool[] preValue1 = new bool[4];
    public bool[] preValue2 = new bool[4];
    public bool[] preValue3 = new bool[4];
    public bool[] preValue4 = new bool[4];
    public bool[] preValue5 = new bool[4];
    #endregion
    public int[] preDirIdx = new int[5];

    public string getDir()
    {
        return direction[dirIndex];
    }

    public string getAct()
    {
        return action[actIndex];
    }

    public bool[] getBoolArray(int i)
    {
        bool[] toSend = new bool[4];

        if (i == 0)
            toSend = preValue1;
        else if (i == 1)
            toSend = preValue2;
        else if (i == 2)
            toSend = preValue3;
        else if (i == 3)
            toSend = preValue4;
        else if (i == 4)
            toSend = preValue5;

        return toSend;
    }

    public void saveBoolArray(int i, bool[] received)
    {
        if (i == 0)
            preValue1 = received;
        else if (i == 1)
            preValue2 = received;
        else if (i == 2)
            preValue3 = received;
        else if (i == 3)
            preValue4 = received;
        else if (i == 4)
            preValue5 = received;
    }
}
