﻿using System;
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
        "AirSlach",
        "AirBoost"
    };
    public string[] actionDescription = new string[] {
        "Slash the opponent with air.",
        "Boost the users speed using air."
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