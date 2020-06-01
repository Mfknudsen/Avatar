using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GetComboInput : MonoBehaviour
{
    private Dictionary<string, KeyCode> buttonCheck;
    private Coroutine currentTimeFrame = null;
    [HideInInspector]
    public Dictionary<string, bool> activeButtons;
    private Vector2 LastMousePos = Vector2.zero, NewMousePos = Vector2.zero;
    private string MouseDir = "Front";

    [Header("Values To Send:")]
    public float timeDelay = 1.0f;
    [Space]
    public string[] KeyNames;
    public KeyCode[] KeyValue;
    [Header("Mouse UI:")]
    public GameObject MouseUI;
    [Space]
    public GameObject[] DirectionVisual;
    [Space]
    public Color[] UI_Colors;

    private void Start()
    {
        foreach (GameObject obj in DirectionVisual)
            obj.GetComponent<Image>().color = UI_Colors[1];

        MouseUI.SetActive(false);

        buttonCheck = new Dictionary<string, KeyCode>();
        activeButtons = new Dictionary<string, bool>();

        for (int i = 0; i < KeyNames.Length; i++)
        {
            buttonCheck.Add(KeyNames[i], KeyValue[i]);
            activeButtons.Add(KeyNames[i], false);
        }
    }
    private void Update()
    {
        foreach (string keyName in buttonCheck.Keys)
        {
            KeyCode inputKey = buttonCheck[keyName];

            if (Input.GetKeyDown(inputKey))
                activeButtons[keyName] = true;
        }

        if (currentTimeFrame == null)
        {
            foreach (bool b in activeButtons.Values)
            {
                if (b)
                {
                    currentTimeFrame = StartCoroutine(InputTimeFrame());
                    break;
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && !MouseUI.activeSelf)
        {
            MouseUI.SetActive(true);
        }
        else if (Input.GetMouseButton(1) && MouseUI.activeSelf)
        {
            if (NewMousePos != LastMousePos)
            {
                Vector2 dir2 = NewMousePos - new Vector2(Screen.width / 2, Screen.height / 2);
                int dirInt = 0;
                LastMousePos = NewMousePos;

                if (Mathf.Abs(dir2.x) < Mathf.Abs(dir2.y))
                {
                    if (dir2.y > 0)
                    {
                        dirInt = 1;
                        MouseDir = "Front";
                    }
                    else
                    {
                        dirInt = 2;
                        MouseDir = "Back";
                    }
                }
                else
                {
                    if (dir2.x > 0)
                    {
                        dirInt = 3;
                        MouseDir = "Right";
                    }
                    else
                    {
                        dirInt = 4;
                        MouseDir = "Left";
                    }
                }

                if (dirInt != 0)
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == dirInt - 1)
                            DirectionVisual[i].GetComponent<Image>().color = UI_Colors[0];
                        else
                            DirectionVisual[i].GetComponent<Image>().color = UI_Colors[1];
                    }
            }
            else
            {
                NewMousePos = Input.mousePosition;
            }
        }
        else
        {
            MouseUI.SetActive(false);
        }
    }

    private IEnumerator InputTimeFrame()
    {
        yield return new WaitForSeconds(timeDelay);

        InputParameters IP = new InputParameters();
        IP.Dir = MouseDir;
        IP.Buttons = activeButtons;

        SendMessage("ActInput", IP);

        string EndValueString = "";
        List<string> s = new List<string>();
        foreach (string keyString in activeButtons.Keys)
        {
            s.Add(keyString);

            if (activeButtons[keyString])
            {
                if (EndValueString == "")
                    EndValueString = keyString;
                else
                    EndValueString += ", " + keyString;
            }
        }

        foreach (string keyString in s)
            activeButtons[keyString] = false;

        Debug.Log(EndValueString + ".    " + MouseDir + ".");

        currentTimeFrame = null;
    }
}

public class InputParameters
{
    public string Dir;
    public Dictionary<string, bool> Buttons;
}