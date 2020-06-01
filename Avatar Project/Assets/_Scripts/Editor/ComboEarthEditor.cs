using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ComboEarth))]
public class ComboEarthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ComboEarth script = (ComboEarth)target;

        script.name = EditorGUILayout.TextField(new GUIContent("Name:"), script.name);

        GUILayout.Space(15);
        displayAction(script);

        GUILayout.Space(15);
        displayRequiredInput(script);

        GUILayout.Space(15);
        displayPreviousInput(script);
    }

    private void displayAction(ComboEarth script)
    {
        GUILayout.Label("Action:", EditorStyles.boldLabel);
        GUIContent arrayLabelAct = new GUIContent("       Function Type:");
        script.actIndex = EditorGUILayout.Popup(arrayLabelAct, script.actIndex, script.action);
        GUILayout.Label("       Function Parameters:", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        EditorGUILayout.Space(20);
        GUILayout.EndHorizontal();
    }

    private void displayRequiredInput(ComboEarth script)
    {
        GUILayout.Label("Required Input:", EditorStyles.boldLabel);
        GUIContent arrayLabelDir = new GUIContent("       Direction:");
        script.dirIndex = EditorGUILayout.Popup(arrayLabelDir, script.dirIndex, script.direction);

        GUILayout.Space(5);
        GUILayout.Label("       Input Values:", EditorStyles.boldLabel);
        foreach (StringBoolDictionary dict in script.requiredInput)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space(20);
            string display = dict.actionName[dict.actIdx];
            EditorGUILayout.LabelField(display);
            dict.value = EditorGUILayout.Toggle(dict.value);
            GUILayout.EndHorizontal();
        }
    }

    private void displayPreviousInput(ComboEarth script)
    {
        GUILayout.Label("Previous Input", EditorStyles.boldLabel);
        script.preIndex = EditorGUILayout.IntSlider(new GUIContent("       Previous Acts Required:"), script.preIndex, 0, 5); ;

        int current = script.previousInput.Count;
        int set = script.preIndex;

        if (current < set)
        {
            for (int i = 0; i < script.preIndex - script.previousInput.Count; i++)
            {
                script.previousInput.Add(new StringBoolDictionary[] { new StringBoolDictionary().pass(0, false), new StringBoolDictionary().pass(1, false), new StringBoolDictionary().pass(2, false), new StringBoolDictionary().pass(3, false) });
            }
        }
        else if (current > set)
        {
            List<StringBoolDictionary[]> toRemove = new List<StringBoolDictionary[]>();

            for (int i = 0; i < current - set; i++)
            {
                if (script.previousInput.Count - i - 1 >= 0)
                    toRemove.Add(script.previousInput[script.previousInput.Count - i - 1]);
            }

            foreach (StringBoolDictionary[] remove in toRemove)
            {
                script.previousInput.Remove(remove);
            }
        }

        for (int i = 0; i < script.previousInput.Count; i++)
        {
            GUILayout.Space(10);
            GUILayout.Label("     Step " + (i + 1), EditorStyles.boldLabel);
            foreach (StringBoolDictionary dict in script.previousInput[i])
            {

                GUILayout.BeginHorizontal();
                EditorGUILayout.Space(20);
                string display = dict.actionName[dict.actIdx];
                EditorGUILayout.LabelField(display);
                dict.value = EditorGUILayout.Toggle(dict.value);
                GUILayout.EndHorizontal();
            }
        }
    }
}
