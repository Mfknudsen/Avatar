using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboEarth : ScriptableObject
{
    public string name = "New Earth Combo";

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
        "BoulderPunch",
        "EarthShield",
        "ThrowWall",
        "LowerWall",
        "RaiseSmallBoulder",
        "KickSmallBoulder",
        "ChargeLow",
        "ChargeHigh"
    };
    public string[] actionDescription = new string[] {
        "Punch a boulder towards the opponent.",
        "Raise a shield of earth in front of yourself.",
        "Raise a small boulder from the ground.",
        "Kick a small boulder that the character is holding.",
        "Charge for a big low attack.",
        "Charge for a big high attack."
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