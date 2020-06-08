using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ComboAir)), System.Serializable]
public class ComboAirEditor : Editor
{
    private Color standardBackgroundColor = new Color(0, 0, 0, 0);
    private Vector2 scrollPos;

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

    public void show(ComboAir script)
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
        GUILayout.BeginVertical();
        GUILayout.Label("Previous Input:", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.Label("Previous Count:", GUILayout.Width(95));
        GUI.backgroundColor = new Color(1, 1, 1, 1);
        script.preIndex = EditorGUILayout.IntPopup(
            script.preIndex,
            new string[] { "0", "1", "2", "3", "4", "5" },
            new int[] { 0, 1, 2, 3, 4, 5 },
            GUILayout.Width(35));
        GUI.backgroundColor = standardBackgroundColor;
        GUILayout.EndHorizontal();

        if (script.preIndex > 0)
        {
            GUI.backgroundColor = new Color(1, 1, 1, 1);
            if (script.preIndex > 3)
                scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
            GUI.backgroundColor = standardBackgroundColor;

            GUILayout.BeginVertical("box");
            for (int i = 0; i < script.preIndex; i++)
            {
                GUILayout.BeginVertical("box");
                GUILayout.BeginHorizontal();

                GUILayout.Space(20);
                GUILayout.Label("Direction:", GUILayout.Width(75));

                GUI.backgroundColor = new Color(1, 1, 1, 1);
                script.preDirIdx[i] = EditorGUILayout.Popup(script.preDirIdx[i], script.direction, GUILayout.Width(75));
                GUI.backgroundColor = standardBackgroundColor;

                GUILayout.EndHorizontal();

                bool[] newValues = script.getBoolArray(i);

                for (int j = 0; j < 4; j++)
                {
                    GUILayout.BeginHorizontal();

                    GUILayout.Space(20);
                    GUILayout.Label(script.requiredInputKeys[j], GUILayout.Width(75));
                    GUILayout.Space(20);

                    GUI.backgroundColor = new Color(1, 1, 1, 1);
                    newValues[j] = EditorGUILayout.Toggle(newValues[j], GUILayout.Width(50));
                    GUI.backgroundColor = standardBackgroundColor;

                    GUILayout.EndHorizontal();

                }

                script.saveBoolArray(i, newValues);

                GUILayout.Space(10);
                GUILayout.EndVertical();
            }
            GUILayout.EndVertical();
            if (script.preIndex > 3)
                GUILayout.EndScrollView();
        }
        GUILayout.EndVertical();
    }
}