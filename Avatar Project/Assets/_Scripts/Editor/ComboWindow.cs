using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UIElements;

[System.Serializable]
public class ComboWindow : EditorWindow
{
    private bool combosUpdated = false;
    private int bendingTypeIdx = 0;
    private bool showBackground = false;
    private string[] bendingTypeNames = new string[] {
        "Air",
        "Water",
        "Earth",
        "Fire"
    };
    private Color[] typeColor = new Color[] {
        new Color(1.5f, 1.5f, 1.5f),
        new Color(0, 0.4f, 0.8f),
        new Color(0.7f, 0.5f, 0),
        new Color(2, 0, 0)
    };
    private Color standardBackgroundColor;
    private Color standardColor;
    private Vector2 scrollPos;
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
        combosUpdated = false;

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

        if (!combosUpdated)
            UpdateOldCombos();

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
        GUI.backgroundColor = new Color(2, 2, 2);
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.normal.textColor = Color.black;
        if (bendingTypeIdx == 0 && AirCombos.Count > 0)
        {
            foreach (ComboAir combo in AirCombos)
            {
                if (GUILayout.Button(combo.name, buttonStyle))
                    selectedAirCombo = combo;
                GUILayout.Space(5);
            }
        }
        else if (bendingTypeIdx == 1 && WaterCombos.Count > 0)
        {
            foreach (ComboWater combo in WaterCombos)
            {
                if (GUILayout.Button(combo.name, buttonStyle))
                    selectedWaterCombo = combo;
                GUILayout.Space(5);
            }
        }
        else if (bendingTypeIdx == 2 && EarthCombos.Count > 0)
        {
            foreach (ComboEarth combo in EarthCombos)
            {
                if (GUILayout.Button(combo.name, buttonStyle))
                    selectedEarthCombo = combo;
                GUILayout.Space(5);
            }
        }
        else if (bendingTypeIdx == 3 && FireCombos.Count > 0)
        {
            foreach (ComboFire combo in FireCombos)
            {
                if (GUILayout.Button(combo.name, buttonStyle))
                    selectedFireCombo = combo;
                GUILayout.Space(5);
            }
        }
    }

    private void drawSelectedComboPart()
    {
        if (bendingTypeIdx == 0)
        {
            if (selectedAirCombo != null)
                AirEditor.show(selectedAirCombo);
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
            AirCombos.Add(CreateInstance("ComboAir") as ComboAir);
        else if (i == 1)
            WaterCombos.Add(CreateInstance("ComboWater") as ComboWater);
        else if (i == 2)
            EarthCombos.Add(CreateInstance("ComboEarth") as ComboEarth);
        else if (i == 3)
            FireCombos.Add(CreateInstance("ComboFire") as ComboFire);
    }

    private void removeSelectedComboPart()
    {
        if (bendingTypeIdx == 0)
            AirCombos.Remove(selectedAirCombo);
        else if (bendingTypeIdx == 1)
            WaterCombos.Remove(selectedWaterCombo);
        else if (bendingTypeIdx == 2)
            EarthCombos.Remove(selectedEarthCombo);
        else if (bendingTypeIdx == 3)
            FireCombos.Remove(selectedFireCombo);

        selectedFireCombo = null;
        selectedAirCombo = null;
        selectedWaterCombo = null;
        selectedEarthCombo = null;
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

    private void UpdateOldCombos()
    {
        int updateCount = 0;

        List<ComboAir> tempAir = new List<ComboAir>();
        for (int i = 0; i < AirCombos.Count; i++)
        {
            updateCount++;

            ComboAir air = AirCombos[i];
            ComboAir newAir = CreateInstance("ComboAir") as ComboAir;

            if (newAir != null && air != null)
            {
                newAir.name = air.name;
                newAir.dirIndex = air.dirIndex;
                newAir.actIndex = air.actIndex;
                newAir.requiredInputValues = air.requiredInputValues;
                newAir.preIndex = air.preIndex;
                newAir.preFuncIdx = air.preFuncIdx;
                newAir.preDirIdx = air.preDirIdx;

                tempAir.Add(newAir);
            }
            else
                Debug.Log("Fail in Air Update");
        }
        AirCombos = tempAir;

        List<ComboWater> tempWater = new List<ComboWater>();
        for (int i = 0; i < WaterCombos.Count; i++)
        {
            updateCount++;

            ComboWater water = WaterCombos[i];
            ComboWater newWater = CreateInstance("ComboWater") as ComboWater;

            if (newWater != null && water != null)
            {
                newWater.name = water.name;
                newWater.dirIndex = water.dirIndex;
                newWater.actIndex = water.actIndex;
                newWater.requiredInputValues = water.requiredInputValues;
                newWater.preIndex = water.preIndex;
                newWater.preFuncIdx = water.preFuncIdx;
                newWater.preDirIdx = water.preDirIdx;

                tempWater.Add(newWater);
            }
            else
                Debug.Log("Fail in Water Update");
        }
        WaterCombos = tempWater;

        List<ComboEarth> tempEarth = new List<ComboEarth>();
        for (int i = 0; i < EarthCombos.Count; i++)
        {
            updateCount++;

            ComboEarth earth = EarthCombos[i];
            ComboEarth newEarth = CreateInstance("ComboEarth") as ComboEarth;

            if (newEarth != null && earth != null)
            {
                newEarth.name = earth.name;
                newEarth.dirIndex = earth.dirIndex;
                newEarth.actIndex = earth.actIndex;
                newEarth.requiredInputValues = earth.requiredInputValues;
                newEarth.preIndex = earth.preIndex;
                newEarth.preFuncIdx = earth.preFuncIdx;
                newEarth.preDirIdx = earth.preDirIdx;

                tempEarth.Add(newEarth);
            }
            else
                Debug.Log("Fail in Earth Update");
        }
        EarthCombos = tempEarth;

        List<ComboFire> tempFire = new List<ComboFire>();
        for (int i = 0; i < FireCombos.Count; i++)
        {
            updateCount++;

            ComboFire fire = FireCombos[i];
            ComboFire newFire = CreateInstance("ComboFire") as ComboFire;

            if (newFire != null && fire != null)
            {
                newFire.name = fire.name;
                newFire.dirIndex = fire.dirIndex;
                newFire.actIndex = fire.actIndex;
                newFire.requiredInputValues = fire.requiredInputValues;
                newFire.preIndex = fire.preIndex;
                newFire.preFuncIdx = fire.preFuncIdx;
                newFire.preDirIdx = fire.preDirIdx;

                tempFire.Add(newFire);
            }
            else
                Debug.Log("Fail in Fire Update");
        }
        FireCombos = tempFire;

        combosUpdated = true;
    }
}
