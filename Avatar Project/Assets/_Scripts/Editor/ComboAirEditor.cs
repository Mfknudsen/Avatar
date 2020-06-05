using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(ComboAir)), System.Serializable]
public class ComboAirEditor : Editor
{
    private Color standardBackgroundColor = new Color(0, 0, 0, 0);
    private Vector2 scrollPos;
    private ComboCollection Collection;

    public override void OnInspectorGUI()
    {
        ComboAir script = (ComboAir)target;

        script.name = EditorGUILayout.TextField(new GUIContent("Name:"), script.name);

        GUILayout.Space(15);
        displayAction(script);

        GUILayout.Space(15);
        displayRequiredInput(script);

        GUILayout.Space(15);
        displayPreviousInput(script);
    }

    public void show(ComboAir script, ref ComboCollection col)
    {
        if (Collection == null)
            Collection = col;

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

    private void displayAction(ComboAir script)
    {
        GUILayout.Label("Action:", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal(GUILayout.MaxWidth(500));
        GUILayout.Space(20);
        GUILayout.Label("Function Type:", GUILayout.Width(100));
        GUI.backgroundColor = new Color(1, 1, 1, 1);
        script.actIndex = EditorGUILayout.Popup(script.actIndex, script.action);
        GUI.backgroundColor = standardBackgroundColor;
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

    private void displayRequiredInput(ComboAir script)
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
        for (int i = 0; i < script.requiredInputKeys.Length; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(30);
            GUI.backgroundColor = standardBackgroundColor;
            GUILayout.Label(script.requiredInputKeys[i], GUILayout.Width(75));
            GUI.backgroundColor = new Color(1, 1, 1);
            bool value = script.requiredInputValues[i];
            script.requiredInputValues[i] = EditorGUILayout.Toggle(value);
            GUI.backgroundColor = standardBackgroundColor;
            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
        GUILayout.EndVertical();
    }

    private void displayPreviousInput(ComboAir script)
    {
        GUILayout.Label("Previous Input", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal("box");
        GUILayout.Space(20);
        GUI.backgroundColor = new Color(1, 1, 1, 1);
        GUILayout.Label("Previous Acts Required:", GUILayout.Width(150));
        script.preIndex = EditorGUILayout.IntPopup(script.preIndex, new string[] { "0", "1", "2", "3", "4", "5" }, new int[] { 0, 1, 2, 3, 4, 5 }, GUILayout.Width(30));
        GUI.backgroundColor = standardBackgroundColor;
        int current = script.preDirIdx.Count;
        int set = script.preIndex;
        GUILayout.EndHorizontal();

        if (current < set)
        {
            int i = set - current;
            for (int j = 0; j < i; j++)
                script.newPrevious(ref Collection);
        }
        else
        {
            for (int i = 0; i < current - set; i++)
                script.removePrevious(i);
        }

        if (set > 2)
        {
            GUI.backgroundColor = new Color(1, 1, 1, 1);
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, true);
            GUI.backgroundColor = standardBackgroundColor;
        }

        if (set > 0)
        {
            if (script.preDirIdx.Count > 0)
            {
                for (int i = 0; i < set; i++)
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
                    int idx = script.preDirIdx[i];
                    string[] dir = script.preDirection[i];
                    script.preDirIdx[i] = EditorGUILayout.Popup(idx, dir, GUILayout.Width(75));
                    GUI.backgroundColor = standardBackgroundColor;
                    GUILayout.EndHorizontal();

                    string[] pInputKeys = script.previousInputKeys[i];
                    bool[] pInputValues = script.previousInputValues[i];

                    GUILayout.Space(10);
                    GUILayout.BeginVertical();
                    for (int t = 0; t < 4; i++)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Space(20);
                        GUI.backgroundColor = standardBackgroundColor;

                        GUILayout.Label(pInputKeys[t], GUILayout.Width(75));
                        GUI.backgroundColor = new Color(1, 1, 1);
                        bool state = pInputValues[t];
                        pInputValues[t] = EditorGUILayout.Toggle(state);

                        GUI.backgroundColor = standardBackgroundColor;
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.EndVertical();
                    GUILayout.EndVertical();
                }
            }
        }

        if (script.preIndex > 2)
            EditorGUILayout.EndScrollView();
    }
}
