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

        for (int i = 0; i < script.SpawnPoints.Length; i++)
        {
            GUILayout.BeginHorizontal("box");

            script.RockNames[i] = EditorGUILayout.TextField(script.RockNames[i], GUILayout.Width(200));
            script.RockGameObjects[i] = (GameObject)EditorGUILayout.ObjectField(script.RockGameObjects[i], typeof(GameObject), true);

            GUILayout.Space(10);
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        for (int i = 0; i < script.SpawnPoints.Length; i++)
        {
            Debug.Log(i);

            script.SpawnPoints[i] = (Transform)EditorGUILayout.ObjectField(script.SpawnPoints[i], typeof(Transform), true);
        }
        GUILayout.EndVertical();
    }
}
