using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public string name;

    public enum Type
    {
        GRUNT,
        BOSS
    }

    public Type EnemyType;

    public float maxHP;
    public float currentHP;

    public float baseATK;
    public float baseDEF;
}