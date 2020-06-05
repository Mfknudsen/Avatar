using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComboCollection : MonoBehaviour
{
    public List<ComboAir> AirCombos;
    public List<ComboWater> WaterCombos;
    public List<ComboEarth> EarthCombos;
    public List<ComboFire> FireCombos;

    #region Starter Values
    [HideInInspector]
    public string[] Directions = new string[] { "Front", "Back", "Left", "Right" };
    [HideInInspector]
    public string[] InputKey = new string[] { "Fast", "Heavy", "Special", "Block" };
    [HideInInspector]
    public bool[] InputValue = new bool[] { false, false, false, false };

    [HideInInspector]
    public string[] AirFunctions = new string[] {
        "AirSlash",
        "AirBoost"
    };
    [HideInInspector]
    public string[] AirDescriptions = new string[] {
        "Slash the opponent with air.",
        "Boost the users speed using air."
    };
    #endregion

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SaveLists(List<ComboAir> airCombos, List<ComboWater> waterCombos, List<ComboEarth> earthCombos, List<ComboFire> fireCombos)
    {
        AirCombos = airCombos;
        WaterCombos = waterCombos;
        EarthCombos = earthCombos;
        FireCombos = fireCombos;
    }
}
