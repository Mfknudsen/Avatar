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
    public int[] preFuncIdx = new int[5];
    public int[] preDirIdx = new int[5];

    public string getDir()
    {
        return direction[dirIndex];
    }

    public string getAct()
    {
        return action[actIndex];
    }
}
