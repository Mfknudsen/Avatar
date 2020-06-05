using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboWater : ScriptableObject
{
    public string name = "New Water Combo";

    public StringBoolDictionary[] requiredInput = new StringBoolDictionary[] { new StringBoolDictionary().pass(0, false), new StringBoolDictionary().pass(1, false), new StringBoolDictionary().pass(2, false), new StringBoolDictionary().pass(3, false) };

    public int dirIndex = 0;
    public string[] direction = new string[] { "Front", "Back", "Left", "Right" };

    public int actIndex = 0;
    public string[] action = new string[] { "GetWater", "WaterSlash" };
    public string[] actionDescription = new string[] {
    "Collect water.",
    "Slash the opponent with water."
    };

    public int preIndex = 0;
    public List<StringBoolDictionary[]> previousInput = new List<StringBoolDictionary[]>();
    public List<int> preDirIdx = new List<int>();
    public List<string[]> preDirection = new List<string[]>();

    public string getDir()
    {
        return direction[dirIndex];
    }

    public string getAct()
    {
        return action[actIndex];
    }

    public Dictionary<string, bool> getReq()
    {
        Dictionary<string, bool> values = new Dictionary<string, bool>();

        for (int i = 0; i < requiredInput.Length; i++)
        {
            StringBoolDictionary req = requiredInput[i];
            string actName = req.actionName[req.actIdx];
            if (actName != "")
                values.Add(actName, req.value);
            else
                return null;
        }

        return values;
    }

    public List<Dictionary<string, bool>> getPre()
    {
        List<Dictionary<string, bool>> values = new List<Dictionary<string, bool>>();

        foreach (StringBoolDictionary[] ray in previousInput)
        {
            Dictionary<string, bool> toAdd = new Dictionary<string, bool>();

            for (int i = 0; i < ray.Length; i++)
            {
                StringBoolDictionary pre = ray[i];
                toAdd.Add(pre.actionName[pre.actIdx], pre.value);
            }

            values.Add(toAdd);
        }

        return values;
    }
}
