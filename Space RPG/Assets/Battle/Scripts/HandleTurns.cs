using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurns
{
    public string Attacker; //name of attacker
    public GameObject AttackersGameObject; //who attacks
    public GameObject AttackersTarget; //who is being attacked

    //which attack is performed
}