using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayableCharacter: BaseClass
{
    /*public float maxHP;
    public float currentHP;

    public float maxSP;
    public float currentSP;*/

    public int stamina;
    public int intellect;
    public int dexterity;
    public int agility;

    public List<BaseAttack> SkillAttacks = new List<BaseAttack>();
}
