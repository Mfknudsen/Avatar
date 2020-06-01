using UnityEngine;
using UnityEditor;

public class ComboWindow : EditorWindow
{
    private int bendingTypeIdx = 2;
    private string[] bendingTypeNames = new string[] { "Air", "Water", "Earth", "Fire" };
    private Color[] typeColor = new Color[] { new Color(1, 1, 1, 0.5f), new Color(0, 0, 1, 0.5f), new Color(1, 0.75f, 0, 0.5f), new Color(1, 0, 0, 0.5f) };
    private Color standardColor;

    public ComboEarth[] EarthCombos;

    [MenuItem("Window/ComboWindow")]
    public static void ShowWindow()
    {
        GetWindow<ComboWindow>("Combo Window");
    }

    private void OnGUI()
    {
        if (standardColor == null)
            standardColor = GUI.backgroundColor;

        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = typeColor[0];
        if (GUILayout.Button("Air"))
            bendingTypeIdx = 0;
        GUI.backgroundColor = typeColor[1];
        if (GUILayout.Button("Water"))
            bendingTypeIdx = 1;
        GUI.backgroundColor = typeColor[2];
        if (GUILayout.Button("Earth"))
            bendingTypeIdx = 2;
        GUI.backgroundColor = typeColor[3];
        if (GUILayout.Button("Fire"))
            bendingTypeIdx = 3;
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUI.backgroundColor = typeColor[bendingTypeIdx];
        GUILayout.Label("Selected: " + bendingTypeNames[bendingTypeIdx]);
        GUI.backgroundColor = standardColor;

        if (GUILayout.Button("Create New Combo Part"))
        {
            createNewComboPart(bendingTypeIdx);
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        drawSidebar();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    private void drawSidebar()
    {
        for (int i = 0; i < 15; i++)
        {
            if (GUILayout.Button("Object " + i)) { }
        }
    }

    private void createNewComboPart(int i)
    {
        if (i == 0)
        {

        }
        else if (i == 1)
        {

        }
        else if (i == 2)
        {

        }
        else if (i == 3)
        {

        }
        else
        {
            Debug.Log("Failed to create new combo part");
        }
    }
}
