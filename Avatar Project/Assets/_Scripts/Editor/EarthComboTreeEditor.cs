using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(EarthComboTree))]
public class EarthComboTreeEditor : Editor
{
    private int RockLength = 0;

    public override void OnInspectorGUI()
    {
        EarthComboTree script = (EarthComboTree)target;

        if (GUILayout.Button("Reset"))
        {
            script.RockNames = new List<string>();
            script.RockGameObjects = new List<GameObject>();
        }

        GUILayout.Space(10);

        RockLength = script.RockNames.Count;

        GUILayout.Space(20);

        GUILayout.Label("Rock Objects:");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
            RockLength++;
        if (GUILayout.Button("Remove"))
            RockLength--;
        GUILayout.EndHorizontal();

        if (RockLength > script.RockNames.Count)
        {
            int lenght = script.RockNames.Count;

            for (int i = 0; i < RockLength - lenght; i++)
            {
                script.RockNames.Add("");
                script.RockGameObjects.Add(null);
            }
        }
        else
        {
            int lenght = script.RockNames.Count;

            for (int i = 0; i < lenght - RockLength; i++)
            {
                script.RockNames.RemoveAt(lenght - i - 1);
                script.RockGameObjects.RemoveAt(lenght - i - 1);
            }
        }

        GUILayout.BeginVertical("box");

        for (int i = 0; i < RockLength; i++)
        {
            GUILayout.BeginHorizontal("box");

            script.RockNames[i] = EditorGUILayout.TextField(script.RockNames[i], GUILayout.Width(150));
            script.RockGameObjects[i] = (GameObject)EditorGUILayout.ObjectField(script.RockGameObjects[i], typeof(GameObject), true);

            GUILayout.Space(10);
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        GUILayout.Space(10);
        GUILayout.Label("Spawn Points:");
        GUILayout.BeginVertical("box");

        if (script.SpawnPoints.Length != 4)
            script.SpawnPoints = new Transform[4];
        string[] Directions = new string[] { "Front", "Back", "Left", "Right" };
        
        for (int i = 0; i < script.SpawnPoints.Length; i++)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(Directions[i], GUILayout.Width(40));
            GUILayout.Space(10);
            script.SpawnPoints[i] = (Transform)EditorGUILayout.ObjectField(script.SpawnPoints[i], typeof(Transform), true);

            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
    }
}
