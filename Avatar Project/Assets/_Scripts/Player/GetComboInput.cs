using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GetComboInput : MonoBehaviour
{
    private Dictionary<string, KeyCode> buttonCheck;
    private Coroutine currentTimeFrame = null;
    [HideInInspector]
    public Dictionary<string, bool> activeButtons;
    private Vector2 LastMousePos = Vector2.zero, NewMousePos = Vector2.zero;

    [Header("KeysToCheck:")]
    public string[] KeyNames;
    public KeyCode[] KeyValue;
    [Space, Header("Mouse UI:")]
    public GameObject MouseUI;

    private void Start()
    {
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

        if (Input.GetMouseButtonDown(0) && !MouseUI.activeSelf)
        {
            MouseUI.SetActive(true);
        }
        else if (Input.GetMouseButton(0) && MouseUI.activeSelf)
        {
            if (NewMousePos != LastMousePos)
            {
                Vector2 dir2 = NewMousePos - LastMousePos;
            } else
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
        yield return new WaitForSeconds(1.0f);

        SendMessage("ActInput", activeButtons);

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

        currentTimeFrame = null;
    }
}
