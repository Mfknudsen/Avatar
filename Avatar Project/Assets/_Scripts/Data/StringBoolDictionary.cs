using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StringBoolDictionary : MonoBehaviour
{
    public int actIdx = 0;
    public string[] actionName = new string[] { "Fast", "Heavy", "Special", "Block" };
    public bool value;

    public StringBoolDictionary pass(int t, bool b)
    {
        StringBoolDictionary SBD = new StringBoolDictionary();

        SBD.actIdx = t;
        SBD.value = b;

        return SBD;
    }
}
