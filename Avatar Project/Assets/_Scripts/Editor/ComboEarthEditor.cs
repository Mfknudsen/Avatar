using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ComboEarth))]
public class ComboEarthEditor : Editor
{
    private Color standardBackgroundColor = new Color(0, 0, 0, 0);

    private Vector2 scrollPos;

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

    public void show(ComboEarth script)
    {
        GUI.backgroundColor = standardBackgroundColor;

        GUILayout.BeginHorizontal("box");

        GUILayout.Label("Name: ", GUILayout.Width(50));

        GUI.backgroundColor = new Color(2, 2, 2, 1);
        script.name = GUILayout.TextField(script.name, 34, GUILayout.Width(250));
        GUI.backgroundColor = standardBackgroundColor;

        if (script.name.Length == 0)
            script.name = "New Combo Part";
        GUILayout.EndHorizontal();

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

        GUILayout.BeginHorizontal(GUILayout.MaxWidth(500));
        GUILayout.Space(20);
        GUILayout.Label("Function Type:", GUILayout.Width(100));
        script.actIndex = EditorGUILayout.Popup(script.actIndex, script.action);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(50);
        GUILayout.Label(script.actionDescription[script.actIndex]);
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.Label("Function Parameters:", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.Space(20);
        GUILayout.EndHorizontal();
    }

    private void displayRequiredInput(ComboEarth script)
    {
        GUILayout.Label("Required Input:", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal("box");
        GUILayout.Space(20);
        GUILayout.Label("Direction:", GUILayout.Width(100));
        GUI.backgroundColor = new Color(1, 1, 1, 1);
        script.dirIndex = EditorGUILayout.Popup(script.dirIndex, script.direction, GUILayout.Width(60)); ;
        GUI.backgroundColor = standardBackgroundColor;
        GUILayout.EndHorizontal();

        GUI.backgroundColor = standardBackgroundColor;

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.Label("Input Values:", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        foreach (StringBoolDictionary dict in script.requiredInput)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            GUI.backgroundColor = standardBackgroundColor;
            GUILayout.Label(dict.actionName[dict.actIdx], GUILayout.Width(75));
            GUI.backgroundColor = new Color(1, 1, 1);
            dict.value = EditorGUILayout.Toggle(dict.value);
            GUI.backgroundColor = standardBackgroundColor;
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        GUILayout.EndVertical();
    }

    private void displayPreviousInput(ComboEarth script)
    {
        GUILayout.Label("Previous Input", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal("box");
        GUILayout.Space(20);
        GUI.backgroundColor = new Color(1, 1, 1, 1);
        GUILayout.Label("Previous Acts Required:", GUILayout.Width(150));
        script.preIndex = EditorGUILayout.IntPopup(script.preIndex, new string[] { "0", "1", "2", "3", "4", "5" }, new int[] { 0, 1, 2, 3, 4, 5 }, GUILayout.Width(30));
        GUI.backgroundColor = standardBackgroundColor;
        int current = script.previousInput.Count;
        int set = script.preIndex;
        GUILayout.EndHorizontal();

        if (current < set)
        {
            for (int i = 0; i < script.preIndex - script.previousInput.Count; i++)
            {
                script.previousInput.Add(new StringBoolDictionary[] { new StringBoolDictionary().pass(0, false), new StringBoolDictionary().pass(1, false), new StringBoolDictionary().pass(2, false), new StringBoolDictionary().pass(3, false) });
                script.preDirIdx.Add(0);
                script.preDirection.Add(new string[] { "Front", "Back", "Left", "Right" });
            }
        }
        else if (current > set)
        {
            List<StringBoolDictionary[]> toRemoveDict = new List<StringBoolDictionary[]>();
            List<int> toRemoveIdx = new List<int>();
            List<string[]> toRemoveDir = new List<string[]>();

            for (int i = 0; i < current - set; i++)
            {
                int n = script.previousInput.Count - i - 1;

                if (script.previousInput.Count - i - 1 >= 0)
                    toRemoveDict.Add(script.previousInput[n]);
                if (script.preDirIdx.Count - i - 1 >= 0)
                    toRemoveIdx.Add(script.preDirIdx[n]);
                if (script.preDirection.Count - i - 1 >= 0)
                    toRemoveDir.Add(script.preDirection[n]);
            }

            foreach (StringBoolDictionary[] remove in toRemoveDict)
                script.previousInput.Remove(remove);
            foreach (int remove in toRemoveIdx)
                script.preDirIdx.Remove(remove);
            foreach (string[] remove in toRemoveDir)
                script.preDirection.Remove(remove);
        }

        if (script.previousInput.Count > 2)
        {
            GUI.backgroundColor = new Color(1, 1, 1, 1);
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, true);
            GUI.backgroundColor = standardBackgroundColor;
        }

        for (int i = 0; i < script.previousInput.Count; i++)
        {
            GUILayout.Space(10);
            GUI.backgroundColor = new Color(0.5f, 0.5f, 0.5f, 1);
            GUILayout.BeginVertical("box");
            GUI.backgroundColor = standardBackgroundColor;
            GUILayout.Label("Step " + (i + 1), EditorStyles.boldLabel, GUILayout.Width(75));

            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            GUILayout.Label("Direction", GUILayout.Width(75));
            GUI.backgroundColor = new Color(1, 1, 1, 1);
            script.preDirIdx[i] = EditorGUILayout.Popup(script.preDirIdx[i], script.preDirection[i], GUILayout.Width(75));
            GUI.backgroundColor = standardBackgroundColor;
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            GUILayout.BeginVertical();
            foreach (StringBoolDictionary dict in script.previousInput[i])
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                GUI.backgroundColor = standardBackgroundColor;
                GUILayout.Label(dict.actionName[dict.actIdx], GUILayout.Width(75));
                GUI.backgroundColor = new Color(1, 1, 1);
                dict.value = EditorGUILayout.Toggle(dict.value);
                GUI.backgroundColor = standardBackgroundColor;
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            GUILayout.EndVertical();
        }

        if (script.previousInput.Count > 2)
            EditorGUILayout.EndScrollView();
    }
}
