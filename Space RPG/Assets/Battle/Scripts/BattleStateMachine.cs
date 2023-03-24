using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }

    public PerformAction battleStates;

    public List<HandleTurns> PerformList = new List<HandleTurns>();
    public List<GameObject> PlayersInBattle = new List<GameObject>();
    public List<GameObject> EnemiesInBattle = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        battleStates = PerformAction.WAIT;
        EnemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        PlayersInBattle.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        switch(battleStates)
        {
            case(PerformAction.WAIT):

            break;
            case(PerformAction.TAKEACTION):

            break;
            case(PerformAction.PERFORMACTION):

            break;
        }
    }

    public void CollectActions(HandleTurns input)
    {
        PerformList.Add(input);
    }
}
