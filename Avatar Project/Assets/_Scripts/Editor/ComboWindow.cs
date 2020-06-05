using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UIElements;

[System.Serializable]
public class ComboWindow : EditorWindow
{
    private int bendingTypeIdx = 2;
    private string[] bendingTypeNames = new string[] { "Air", "Water", "Earth", "Fire" };
    private Color[] typeColor = new Color[] { new Color(0.75f, 0.75f, 0.75f, 1), new Color(0, 0, 1, 1), new Color(1, 0.75f, 0, 1), new Color(1, 0, 0, 1) };
    private Color standardBackgroundColor;
    private Color standardColor;
    private Vector2 scrollPos;
    private bool showBackground = false;

    private ComboCollection collection = null;

    #region Combo Values
    public List<ComboAir> AirCombos;
    private ComboAir selectedAirCombo = null;
    private ComboAirEditor AirEditor = null;

    public List<ComboWater> WaterCombos;
    private ComboWater selectedWaterCombo = null;
    private ComboWaterEditor WaterEditor = null;

    public List<ComboEarth> EarthCombos;
    private ComboEarth selectedEarthCombo = null;
    private ComboEarthEditor EarthEditor = null;

    public List<ComboFire> FireCombos;
    private ComboFire selectedFireCombo = null;
    private ComboFireEditor FireEditor = null;
    #endregion

    [MenuItem("Window/ComboWindow")]
    public static void ShowWindow()
    {
        GetWindow<ComboWindow>("Combo Window");
    }

    private void OnEnable()
    {
        collection = GameObject.FindGameObjectWithTag("BendingSystem").GetComponent<ComboCollection>();

        if (collection == null)
            Debug.Log("Null");

        if (standardBackgroundColor == null)
            standardBackgroundColor = GUI.backgroundColor;
        if (standardColor == null)
            standardColor = GUI.color;

        if (AirCombos == null)
            AirCombos = new List<ComboAir>();
        if (FireEditor == null)
            AirEditor = CreateInstance("ComboAirEditor") as ComboAirEditor;

        if (WaterCombos == null)
            WaterCombos = new List<ComboWater>();
        if (WaterEditor == null)
            WaterEditor = CreateInstance("ComboWaterEditor") as ComboWaterEditor;

        if (EarthCombos == null)
            EarthCombos = new List<ComboEarth>();
        if (EarthEditor == null)
            EarthEditor = CreateInstance("ComboEarthEditor") as ComboEarthEditor;

        if (FireCombos == null)
            FireCombos = new List<ComboFire>();
        if (FireEditor == null)
            FireEditor = CreateInstance("ComboFireEditor") as ComboFireEditor;
    }

    private void OnGUI()
    {
        if (collection == null)
            collection = GameObject.FindGameObjectWithTag("BendingSystem").GetComponent<ComboCollection>();

        LoadLists();

        drawTopMenu();

        GUI.backgroundColor = standardBackgroundColor;

        EditorGUILayout.BeginHorizontal("box");

        showBackground = false;
        if (bendingTypeIdx == 0)
        {
            if (AirCombos.Count > 0)
                showBackground = true;
        }
        else if (bendingTypeIdx == 1)
        {
            if (WaterCombos.Count > 0)
                showBackground = true;
        }
        else if (bendingTypeIdx == 2)
        {
            if (EarthCombos.Count > 0)
                showBackground = true;
        }
        else if (bendingTypeIdx == 3)
        {
            if (FireCombos.Count > 0)
                showBackground = true;
        }

        if (showBackground)
        {
            GUI.backgroundColor = new Color(0.5f, 0.5f, 0.5f);
            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(250));
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
            drawSidebar();
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            GUI.backgroundColor = standardBackgroundColor;
        }

        GUILayout.Space(20);

        if (selectedAirCombo != null || selectedWaterCombo != null || selectedEarthCombo != null || selectedFireCombo != null)
        {
            GUI.backgroundColor = new Color(0.5f, 0.5f, 0.5f, 1);
            EditorGUILayout.BeginVertical("box");
            drawSelectedComboPart();
            EditorGUILayout.EndVertical();
            GUI.backgroundColor = standardBackgroundColor;
        }
        EditorGUILayout.EndHorizontal();

        SaveLists();
    }

    private void drawTopMenu()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = typeColor[0];
        if (GUILayout.Button("Air"))
        {
            bendingTypeIdx = 0;
            selectedWaterCombo = null;
            selectedEarthCombo = null;
            selectedFireCombo = null;
        }
        GUI.backgroundColor = typeColor[1];
        if (GUILayout.Button("Water"))
        {
            bendingTypeIdx = 1;
            selectedAirCombo = null;
            selectedEarthCombo = null;
            selectedFireCombo = null;
        }
        GUI.backgroundColor = typeColor[2];
        if (GUILayout.Button("Earth"))
        {
            bendingTypeIdx = 2;
            selectedAirCombo = null;
            selectedWaterCombo = null;
            selectedFireCombo = null;
        }
        GUI.backgroundColor = typeColor[3];
        if (GUILayout.Button("Fire"))
        {
            bendingTypeIdx = 3;
            selectedAirCombo = null;
            selectedWaterCombo = null;
            selectedEarthCombo = null;
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUI.backgroundColor = standardBackgroundColor;

        GUI.backgroundColor = typeColor[bendingTypeIdx];
        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Selected: " + bendingTypeNames[bendingTypeIdx], GUILayout.Width(95));
        GUILayout.EndHorizontal();
        GUI.backgroundColor = standardBackgroundColor;

        EditorGUILayout.BeginHorizontal("box");
        GUI.backgroundColor = new Color(0, 1, 0);
        if (GUILayout.Button("Create New Combo Part"))
            createNewComboPart(bendingTypeIdx);

        GUI.backgroundColor = new Color(1, 0, 0);
        if (GUILayout.Button("Remove Selected Combo Part"))
            removeSelectedComboPart();

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private void drawSidebar()
    {
        if (bendingTypeIdx == 0 && AirCombos.Count > 0)
        {
            foreach (ComboAir combo in AirCombos)
            {
                if (GUILayout.Button(combo.name))
                    selectedAirCombo = combo;
            }
        }
        else if (bendingTypeIdx == 1 && WaterCombos.Count > 0)
        {
            foreach (ComboWater combo in WaterCombos)
            {
                if (GUILayout.Button(combo.name))
                    selectedWaterCombo = combo;
            }
        }
        else if (bendingTypeIdx == 2 && EarthCombos.Count > 0)
        {
            foreach (ComboEarth combo in EarthCombos)
            {
                if (GUILayout.Button(combo.name))
                    selectedEarthCombo = combo;
            }
        }
        else if (bendingTypeIdx == 3 && FireCombos.Count > 0)
        {
            foreach (ComboFire combo in FireCombos)
            {
                if (GUILayout.Button(combo.name))
                    selectedFireCombo = combo;
            }
        }
    }

    private void drawSelectedComboPart()
    {
        if (bendingTypeIdx == 0)
        {
            if (selectedAirCombo != null)
                AirEditor.show(selectedAirCombo, ref collection);
        }
        else if (bendingTypeIdx == 1)
        {
            if (selectedWaterCombo != null)
                WaterEditor.show(selectedWaterCombo);
        }
        else if (bendingTypeIdx == 2)
        {
            if (selectedEarthCombo != null)
                EarthEditor.show(selectedEarthCombo);
        }
        else if (bendingTypeIdx == 3)
        {
            if (selectedFireCombo != null)
                FireEditor.show(selectedFireCombo);
        }
    }

    private void createNewComboPart(int i)
    {
        if (i == 0)
        {
            ComboAir air = CreateInstance("ComboAir") as ComboAir;

            ComboCollection col = new ComboCollection();

            air.requiredInputKeys =  col.InputKey;
            air.requiredInputValues = col.InputValue;

            air.actIndex = 0;
            air.direction = col.Directions;

            air.actIndex = 0;
            air.action = col.AirFunctions;
            air.actionDescription = col.AirDescriptions;

            air.preIndex = 0;
            air.previousInputKeys = new List<string[]>();
            air.previousInputValues = new List<bool[]>();
            air.preDirIdx = new List<int>();
            air.preDirection = new List<string[]>();

            AirCombos.Add(air);
        }
        else if (i == 1)
        {
            WaterCombos.Add(CreateInstance("ComboWater") as ComboWater);
        }
        else if (i == 2)
        {
            EarthCombos.Add(CreateInstance("ComboEarth") as ComboEarth);
        }
        else if (i == 3)
        {
            FireCombos.Add(CreateInstance("ComboFire") as ComboFire);
        }
        else
            Debug.Log("Failed to create new combo part");
    }

    private void removeSelectedComboPart()
    {
        if (bendingTypeIdx == 0)
        {
            AirCombos.Remove(selectedAirCombo);
            selectedAirCombo = null;
        }
        else if (bendingTypeIdx == 1)
        {
            WaterCombos.Remove(selectedWaterCombo);
            selectedWaterCombo = null;
        }
        else if (bendingTypeIdx == 2)
        {
            EarthCombos.Remove(selectedEarthCombo);
            selectedEarthCombo = null;
        }
        else if (bendingTypeIdx == 3)
        {
            FireCombos.Remove(selectedFireCombo);
            selectedFireCombo = null;
        }
        else
        {
            Debug.Log("Failed to remove combo part");
        }
    }

    private void SaveLists()
    {
        collection.SaveLists(AirCombos, WaterCombos, EarthCombos, FireCombos);
    }

    private void LoadLists()
    {
        AirCombos = new List<ComboAir>();
        WaterCombos = new List<ComboWater>();
        EarthCombos = new List<ComboEarth>();
        FireCombos = new List<ComboFire>();

        AirCombos = collection.AirCombos;
        WaterCombos = collection.WaterCombos;
        EarthCombos = collection.EarthCombos;
        FireCombos = collection.FireCombos;
    }

    private void ResetList()
    {
        AirCombos = new List<ComboAir>();
        WaterCombos = new List<ComboWater>();
        EarthCombos = new List<ComboEarth>();
        FireCombos = new List<ComboFire>();
    }
}
