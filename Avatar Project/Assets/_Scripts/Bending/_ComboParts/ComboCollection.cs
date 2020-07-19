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

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SaveLists(List<ComboAir> airCombos, List<ComboWater> waterCombos, List<ComboEarth> earthCombos, List<ComboFire> fireCombos)
    {
        AirCombos = airCombos;
        WaterCombos = waterCombos;
        EarthCombos = earthCombos;
        FireCombos = fireCombos;
    }
}
