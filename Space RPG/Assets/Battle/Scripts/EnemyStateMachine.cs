using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM; 
    public Enemy enemy;

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
    public GameObject Selector;

    //timeforaction stuff
    private bool actionStarted = false;
    public GameObject PlayerToAttack;
    private float animSpeed = 5f;

    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        currentState = TurnState.PROCESSING;
        Selector.SetActive(false);
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
                StartCoroutine(TimeForAction());

            break;
            case(TurnState.DEAD):
                if(!alive)
                {
                    return;
                }
                else
                {
                    this.gameObject.tag = "DeadEnemy";
                    BSM.EnemiesInBattle.Remove(this.gameObject);
                    Selector.SetActive(false);

                    if(BSM.EnemiesInBattle.Count > 0)
                    {
                        for(int i = 0; i < BSM.PerformList.Count; i++)
                        {
                            if (i != 0)
                            {
                                if(BSM.PerformList[i].AttackersGameObject == this.gameObject)
                                {
                                    BSM.PerformList.Remove(BSM.PerformAction[i]);
                                }

                                if(BSM.PerformList[i].AttackersTarget == this.gameObject)
                                {
                                    BSM.PerformList[i].AttackersTarget = BSM.EnemiesInBattle[Random.Range(0, BSM.EnemiesInBattle.Count)];
                                }
                            }
                        }
                    }

                    this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(105, 105, 105, 255);
                    alive = false;
                    BSM.EnemyButtons();
                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
                }

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
        myAttack.Attacker = enemy.theName;
        myAttack.Recon = "Enemy";
        myAttack.AttackersGameObject = this.gameObject;
        myAttack.AttackersTarget = BSM.PlayersInBattle [Random.Range (0, BSM.PlayersInBattle.Count)];

        int num = Random.Range(0, enmey.attacks.Count);
        myAttack.chosenAttack = enemy.attacks[num];
        Debug.Log (this.gameObject.name + " has chosen " + myAttack.chosenAttack.attackName + " and do " + myAttack.chosenAttack.attackDamage + " damage!");

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
        Vector2 playerPosition = new Vector2 (PlayerToAttack.transform.position.x - 1.5f, PlayerToAttack.transform.position.y, PlayerToAttack.transform.position.z);
        while(MoveTowardsEnemy(playerPosition))
        {
            yield return null;
        }

        //wait a bit
        yield return new WaitForSeconds(0.5f);
        //do damage
        DoDamage();

        //animate back to startposition
        Vector2 firstPosition = startposition;
        while(MoveTowardsStart(firstPosition))
        {
            yield return null;
        }

        //remove this performer from the list in BSM
        BSM.PerformList.RemoveAt(0);

        //resest BSM -> Wait
        BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
        //end coroutine

        actionStarted = false;
        //reset this enemy state
        cur_cooldown = 0f;
        currentState = TurnState.PROCESSING;
    }

    private bool MoveTowardsEnemy(Vector2 target)
    {
        return target != (transform.position = Vector2.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector2 target)
    {
        return target != (transform.position = Vector2.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    void DoDamage()
    {
        float calc_damage = enemy.curATK + BSM.PerformList[0].chosenAttack.attackDamage;
        PlayerToAttack.GetComponent<PlayerStateMachine>().TakeDamage(calc_damage);
    }

    public void TakeDamage(float getDamageAmount)
    {
        enemy.currentHP -= getDamageAmount;
        if(enemy.currentHP <= 0)
        {
            enemy.currentHP = 0;
            currentState = TurnState.DEAD;
        }
    }
}
