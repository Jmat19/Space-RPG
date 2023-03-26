using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
    public string theName;

    public float maxHP;
    public float currentHP;

    public float maxSP;
    public float currentSP;

    public float baseATK;
    public float curATK;
    public float baseDEF;
    public float curDEF;

    public List<BaseAttack> attacks = new List<BaseAttack>();
}
