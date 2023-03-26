using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;
    public PlayableCharacter player;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    //For progress bar
    private float cur_cooldown = 0f;
    private float max_cooldown = 4f;
    public Image ProgressBar;
    public GameObject Selector;

    private bool alive = true;

    private PlayerPanelStats stats;
    public GameObject PlayerPanel;
    private Transform PlayerPanelSpacer;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPanelSpacer = GameObject.Find("BattleCanvas").transform.FindChild("PlayerPanel").transform.FindChild("PlayerPanelSpacer");
        CreatePlayerPanel();

        cur_cooldown = Random.Range(0, 2.5f);
        Selector.SetActive(false);
        BSM = GameObject.Find("Battle Manager").GetComponent<BattleStateMachine>();
        currentState = TurnState.PROCESSING;
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
            case(TurnState.ADDTOLIST):
                BSM.PlayersToManage.Add(this.GameObject);
                currentState = TurnState.WAITING;

            break;
            case(TurnState.WAITING):
                //idle

            break;

            case(TurnState.ACTION):

            break;
            case(TurnState.DEAD):
                if(!alive)
                {
                    return;
                }
                else
                {
                    this.gameObject.tag = "DeadPlayer";
                    BSM.PlayersInBattle.Remove(this.gameObject);
                    BSM.PlayersToManage.Remove(this.gameObject);
                    Selector.SetActive(false);
                    BSM.AttackPanel.SetActive(false);
                    BSM.EnemySelectPanel.SetActive(false);

                    if (BSM.PlayersInBattle.Count > 0 )
                    {
                        for (int i = 0; i < BSM.PerformList.Count; i++)
                        {
                            if (i != 0)
                            {
                                if(BSM.PerformList[i].AttackersGameObject == this.gameObject)
                                {
                                    BSM.PerformList.Remove(BSM.PerformList[i]);
                                }

                                if(BSM.PerformList[i].AttackersTarget == this.gameObject)
                                {
                                    BSM.PerformList[i].AttackersTarget = BSM.PlayersInBattle[Random.Range(0, BSM.PlayersInBattle.Count)];
                                }
                            }
                        }
                    }

                    //To possibly remove in case this doesn't work in 2D
                    this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(105,105,105,255);

                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
                    alive = false;
                }

            break;
        }
    }

    //DoDamage(); -> In case I need to put the player movement code
    //Trigger win or lose battle states

    void UpgradeProgressBar()
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;
        float calc_cooldown = cur_cooldown / max_cooldown;
        ProgressBar.transform.localScale = new Vector2(Mathf.Clamp(calc_cooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);

        if (cur_cooldown >= max_cooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }

    public void TakeDamage(float getDamageAmount)
    {
        player.currentHP -= getDamageAmount;
        if(player.currentHP <= 0)
        {
            player.currentHP = 0;
            currentState = TurnState.DEAD;
        }
        UpdatePlayerPanel();
    }

    void DoDamge()
    {
        float calc_damage = player.curATK + BSM.PerformList[0].chosenAttack.attackDamage;
        EnemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calc_damage);
    }

    void CreatePlayerPanel()
    {
        PlayerPanel = Instantiate(PlayerPanel) as GameObject;
        stats = PlayerPanel.GetComponent<PlayerPanelStats>();
        stats.PlayerName.text = hero.theName;
        stats.PlayerHP.text = "HP: " + player.currentHP;
        stats.PlayerSP.text = "SP: " + player.currentSP;
        
        ProgressBar = stats.ProgressBar;

        PlayerPanel.transform.SetParent(PlayerPanelSpacer, false);
    }

    void UpdatePlayerPanel()
    {
        stats.PlayerHP.text = "HP: " + player.currentHP;
        stats.PlayerSP.text = "SP: " + player.currentSP;
    }
}
