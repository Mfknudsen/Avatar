using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboAir : ScriptableObject
{
    public string name = "New Air Combo";

    public string[] requiredInputKeys = new string[] { 
        "Fast", 
        "Heavy", 
        "Special", 
        "Block" 
    };
    public bool[] requiredInputValues = new bool[] { 
        false, 
        false, 
        false, 
        false 
    };

    public int dirIndex = 0;
    public string[] direction = new string[] { 
        "Front", 
        "Back", 
        "Left", 
        "Right" 
    };

    public int actIndex = 0;
    public string[] action = new string[] {
        "AirSlach",
        "AirBoost"
    };
    public string[] actionDescription = new string[] {
        "Slash the opponent with air,",
        "Boost the users speed using air."
    };

    public int preIndex;
    public List<string[]> previousInputKeys = new List<string[]> {
        new string[] { "Fast", "Heavy", "Special", "Block" },
        new string[] { "Fast", "Heavy", "Special", "Block" },
        new string[] { "Fast", "Heavy", "Special", "Block" },
        new string[] { "Fast", "Heavy", "Special", "Block" },
        new string[] { "Fast", "Heavy", "Special", "Block" },
    };
    public List<bool[]> previousInputValues = new List<bool[]> {
        new bool[] { false, false, false, false },
        new bool[] { false, false, false, false },
        new bool[] { false, false, false, false },
        new bool[] { false, false, false, false },
        new bool[] { false, false, false, false }
    };
    public List<int> preDirIdx = new List<int> {
        0,
        0,
        0,
        0,
        0
    };
    public List<string[]> preDirection = new List<string[]> {
        new string[] { "Front", "Back", "Left", "Right" },
        new string[] { "Front", "Back", "Left", "Right" },
        new string[] { "Front", "Back", "Left", "Right" },
        new string[] { "Front", "Back", "Left", "Right" },
        new string[] { "Front", "Back", "Left", "Right" }
    };

    public string getDir()
    {
        return direction[dirIndex];
    }

    public string getAct()
    {
        return action[actIndex];
    }
}
