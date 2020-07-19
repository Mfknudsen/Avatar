using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboFire : ScriptableObject
{
    public string name = "New Fire Combo";

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
        "FireFist",
        "FireSchyte"
    };
    public string[] actionDescription = new string[] {
        "Use your fist to send a fireball towards the opponent.",
        "Send fire in a large arc towards the opponentaaaaaaaaaaaaaaaaaaaaaaaaaa."
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
