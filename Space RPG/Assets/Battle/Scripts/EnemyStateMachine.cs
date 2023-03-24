using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM; 
    public Enemy alien;

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    //For progress bar
    private float cur_cooldown = 0f;
    private float max_cooldown = 4f;

    //this gameobject
    private Vector2 start;

    //timeforaction stuff
    private bool actionStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("Battle Manager").GetComponent<BattleStateMachine>();
        startposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.log(currentState);
        switch(currentState)
        {
            case(TurnState.PROCESSING):
                UpgradeProgressBar();
            
            break;
            case(TurnState.CHOOSEACTION):
                ChooseAction();
                currentState = TurnState.WAITING;

            break;
            case(TurnState.WAITING):
                //idle state
            break;
            case(TurnState.ACTION):

            break;
            case(TurnState.DEAD):

            break;
        }
    }

    void UpgradeProgressBar()
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;

        if (cur_cooldown >= max_cooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }

    void ChooseAction()
    {
        HandleTurns myAttack = new HandleTurns();
        myAttack.Attacker = alien.name;
        myAttack.AttackersGameObject = this.gameObject;
        myAttack.AttackersTarget = BSM.PlayersInBattle [Random.Range (0, BSM.PlayersInBattle.Count)];
        BSM.CollectActions(myAttack);
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        //animate the enemy

        //wait a bit
        //do damage

        //animate back to startposition
    }
}
