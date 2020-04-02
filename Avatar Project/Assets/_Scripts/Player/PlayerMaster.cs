#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#endregion

public enum PlayerState { Idle = 0, Moving = 1, Bending = 2, Staggered = 3, Dead = 4 }
public class PlayerMaster : MonoBehaviour
{
    [Header("Inputs")]
    public KeyCode AbilityKey1;
    public KeyCode AbilityKey2;
    public KeyCode AbilityKey3;

    [Header("Attacks:")]
    public Move GetRock;
    public List<Combo> Combos;

    [Header("Components:")]
    public Animator Anime;

    void Start()
    {
        Anime = GetComponent<Animator>();

        PrimeAttacks();
    }

    void PrimeAttacks()
    {

    }
}

[System.Serializable]
public class Move
{
    public string name;
    public float animeLength;
}