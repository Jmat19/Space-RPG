using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public BaseAttack skillAttackToPerform;

    public void CastSkillAttack()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input4(skillAttackToPerform);
    }
}
