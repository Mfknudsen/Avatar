#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#endregion

public enum AttackType { Ability1 = 0, Ability2 = 1, Ability3 = 2 };
public class Moves : MonoBehaviour
{

    [Header("Inputs:")]
    public KeyCode Key1;
    public KeyCode Key2;
    public KeyCode Key3;

    [Header("Attack:")]
    public Attack heavyAttack;
    public Attack lightAttack;
    public Attack kickAttack;
    public List<Combo> combos;
    public float comboLeeway = 0.25f;

    [Header("Components:")]
    public Animator Anim;

    Attack curAttack = null;
    ComboInput lastInput = null;
    List<int> currentCombos = new List<int>();

    float timer = 0;
    float leeway = 0;
    bool skip = false;

    private void Start()
    {
        Anim = GetComponent<Animator>();
        PrimeCombos();
    }

    void PrimeCombos()
    {
        for (int i = 0; i < combos.Count; i++)
        {
            Combo c = combos[i];
            c.onInputted.AddListener(() =>
            {
            //Call attack function with the combo's attack.
            skip = true;
                Attack(c.comboAttack);
                ResetCombos();
            });
        }
    }

    private void Update()
    {
        if (curAttack != null)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
                curAttack = null;

            return;
        }

        if (currentCombos.Count > 0)
        {
            leeway += Time.deltaTime;

            if (leeway >= comboLeeway)
            {
                if (lastInput != null)
                {
                    Attack(GetAttackFromType(lastInput.type));
                    lastInput = null;
                }

                ResetCombos();
            }
        }
        else
            leeway = 0;

        ComboInput input = null;
        if (Input.GetKeyDown(Key1))
            input = new ComboInput(AttackType.Ability1);
        if (Input.GetKeyDown(Key2))
            input = new ComboInput(AttackType.Ability2);
        if (Input.GetKeyDown(Key3))
            input = new ComboInput(AttackType.Ability3);

        if (input == null) return;

        lastInput = input;

        List<int> remove = new List<int>();
        for (int i = 0; i < currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];

            if (c.continueCombo(input))
                leeway = 0;
            else
                remove.Add(i);
        }

        if (skip)
        {
            skip = false;
            return;
        }

        for (int i = 0; i < combos.Count; i++)
        {
            if (currentCombos.Contains(i)) continue;
            if (combos[i].continueCombo(input))
            {
                currentCombos.Add(i);
                leeway = 0;
            }
        }

        foreach (int i in remove)
            currentCombos.RemoveAt(i);

        if (currentCombos.Count <= 0)
            Attack(GetAttackFromType(input.type));
    }

    void ResetCombos()
    {
        for (int i = 0; i < currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];
            c.ResetCombo();
        }
    }

    void Attack(Attack att)
    {
        curAttack = att;
        timer = att.length;
        Anim.Play(att.name, -1, 0);
    }

    Attack GetAttackFromType(AttackType t)
    {
        if (t == AttackType.Ability1)
            return heavyAttack;
        if (t == AttackType.Ability2)
            return lightAttack;
        if (t == AttackType.Ability3)
            return kickAttack;

        return null;
    }
}

[System.Serializable]
public class Attack
{
    public string name;
    public float length;
}

[System.Serializable]
public class ComboInput
{
    public AttackType type;

    public ComboInput(AttackType t)
    {
        type = t;
    }

    public bool isSameAs(ComboInput test)
    {
        return (type == test.type);
    }
}

[System.Serializable]
public class Combo
{
    public string name;
    public List<ComboInput> inputs;
    public Attack comboAttack;
    public UnityEvent onInputted;
    int curInput = 0;

    public bool continueCombo(ComboInput i)
    {
        if (inputs[curInput].isSameAs(i))
        {
            curInput++;
            if (curInput >= inputs.Count) //Finished the inputs and we should do the attack.
            {
                onInputted.Invoke();
                curInput = 0;
            }
            return true;
        }
        else
        {
            curInput = 0;
            return false;
        }
    }

    public ComboInput currentComboInput()
    {
        if (curInput >= inputs.Count) return null;
        return inputs[curInput];
    }

    public void ResetCombo()
    {
        curInput = 0;
    }
}

class Temp : MonoBehaviour
{
    #region Public Data
    [Header("Rocks:")]
    public GameObject TinyRock;
    public GameObject RockWall;
    [Header("Characters:")]
    public GameObject TestDummy;
    public GameObject Player;
    #endregion

    #region Private Data
    private List<GameObject> CurrentRocks = new List<GameObject>();
    bool ReadyForNextMove = true;
    Dictionary<string, bool> Inputs = new Dictionary<string, bool>();
    private int ComboCounter = 1;
    Coroutine CurrentAtion = null;
    #endregion

    private void Start()
    {
        Inputs.Add("Key1", false);
        Inputs.Add("Key2", false);

    }

    private void Update()
    {
        if (ReadyForNextMove)
        {
            List<string> strings = new List<string>();
            foreach (string key in Inputs.Keys)
                strings.Add(key);
            foreach (string s in strings)
                Inputs[s] = false;

            if (Input.anyKey)
            {
                CurrentAtion = StartCoroutine(WaitForExtraInput());
            }
            else if (CurrentAtion == StartCoroutine(WaitForExtraInput()))
            {
                StopCoroutine(CurrentAtion);
                Inputs.Clear();
                CurrentAtion = null;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !Inputs["Key1"])
                Inputs["Key1"] = true;
            if (Input.GetKeyDown(KeyCode.Alpha2) && !Inputs["Key2"])
                Inputs["Key2"] = true;
        }
    }

    private IEnumerator WaitForExtraInput()
    {
        ReadyForNextMove = false;

        yield return new WaitForSecondsRealtime(0.25f);

        CurrentAtion = null;

        if (CurrentRocks.Count == 0)
        {
            if (Inputs["Key1"] && Inputs["Key2"])
                CurrentAtion = StartCoroutine(CreateRockWall());
            else if (Inputs["Key1"])
                CurrentAtion = StartCoroutine(LiftRock1());
            else
                ReadyForNextMove = true;
        }
        else
        {
            if (Inputs["Key2"])
                CurrentAtion = StartCoroutine(PunchRock1());
            else
                ReadyForNextMove = true;
        }
    }

    private IEnumerator LiftRock1()
    {
        Vector3 readyPos = Player.transform.position + new Vector3(0, 1f, 0) + Player.transform.forward * 1.5f;
        GameObject obj = Instantiate(TinyRock, Player.transform.position - new Vector3(0, 1, 0) + Player.transform.forward * 1.5f, Player.transform.rotation);
        CurrentRocks.Add(obj);
        obj.GetComponent<BoxCollider>().enabled = false;

        while (Vector3.Distance(obj.transform.position, readyPos) > 0.2f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, readyPos, 0.1f);
            yield return null;
        }

        ReadyForNextMove = true;

        CurrentAtion = null;
    }

    private IEnumerator PunchRock1()
    {
        Vector3 target = TestDummy.transform.position;
        GameObject obj = CurrentRocks[0];
        Vector3 startPos = obj.transform.position;
        float moveSpeed = 5f;

        float AnimTimer = 0;

        while (obj != null)
        {
            AnimTimer += Time.deltaTime * moveSpeed;
            AnimTimer = AnimTimer % 5f;

            obj.transform.position = MathParabola.Parabola(startPos, target, 5, AnimTimer / 5f, ComboCounter);

            if (obj.GetComponent<BoxCollider>().bounds.Intersects(TestDummy.GetComponent<CapsuleCollider>().bounds))
            {
                ComboCounter *= -1;
                Destroy(obj);
                CurrentRocks.Remove(obj);
                obj = null;
            }

            yield return null;
        }

        ReadyForNextMove = true;

        CurrentAtion = null;
    }

    private IEnumerator CreateRockWall()
    {
        ReadyForNextMove = false;
        bool isWallUp = false;
        bool removeWall = false;
        GameObject obj = Instantiate(RockWall, Player.transform.position - new Vector3(0, 3f, 0) + Player.transform.forward * 2, Player.transform.rotation);
        Vector3 startPos = obj.transform.position;
        Vector3 endPos = obj.transform.position + obj.transform.up * 3.5f;

        while (obj != null)
        {
            if (!isWallUp)
            {
                if (Vector3.Distance(obj.transform.position, endPos) > 0.2f)
                    obj.transform.position = Vector3.Lerp(obj.transform.position, endPos, 0.05f);
                else
                    isWallUp = true;
            }
            else
            {
                if (!removeWall)
                {
                    if (!Input.GetKey(KeyCode.Alpha2) || !Input.GetKey(KeyCode.Alpha1))
                        removeWall = true;
                }
                else
                {
                    if (Vector3.Distance(obj.transform.position, startPos) > 0.2f)
                    {
                        obj.transform.position = Vector3.Lerp(obj.transform.position, startPos, 0.05f);
                    }
                    else
                    {
                        ReadyForNextMove = true;
                        Destroy(obj);
                    }
                }
            }
            yield return null;
        }
    }
}

