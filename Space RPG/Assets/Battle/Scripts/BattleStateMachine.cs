using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION,
        CHECKALIVE,
        WIN,
        LOSE
    }

    public PerformAction battleStates;

    public List<HandleTurns> PerformList = new List<HandleTurns>();
    public List<GameObject> PlayersInBattle = new List<GameObject>();
    public List<GameObject> EnemiesInBattle = new List<GameObject>();

    public enum PlayerGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        INPUT3,
        DONE
    }

    public PlayerGUI PlayerInput;

    public List<GameObject> PlayersToManage = new List<GameObject>();
    private HandleTurns PlayerChoice;

    public GameObject enemyButton;
    public Transform Spacer;

    public GameObject AttackPanel;
    public GameObject EnemySelectPanel;
    public GameObject SkillPanel;

    public Transform actionSpacer;
    public Transform skillSpacer;
    public GameObject actionButton;
    public GameObject skillButton;
    private List<GameObject> atkBtns = new List<GameObject>();

    private List<GameObject> enemyBtns = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        battleStates = PerformAction.WAIT;
        EnemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        PlayersInBattle.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        PlayerInput = PlayerGUI.ACTIVATE;

        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(false);
        SkillPanel.SetActive(false);

        EnemyButtons();
    }

    // Update is called once per frame
    void Update()
    {
        switch(battleStates)
        {
            case(PerformAction.WAIT):
                if(PerformList.Couny > 0)
                {
                    battleStates = PerformAction.TAKEACTION;
                }

            break;
            case(PerformAction.TAKEACTION):
                GameObject performer = GameObject.Find(PerformList[0].Attacker)
                if(PerformList[0].Recon == "Enemy")
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    for(int i = 0; i < PlayersInBattle.Count; i++)
                    {
                        if(PerformList[0].AttackersTarget == PlayersInBattle[i])
                        {
                            ESM.PlayerToAttack = PerformList[0].AttackersTarget;
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                            break;
                        }
                        else
                        {
                            PerformList[0].AttackersTarget = PlayersInBattle[Random.Range(0, PlayersInBattle.Count)];\
                            ESM.PlayerToAttack = PerformList[0].AttackersTarget;
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                        }
                    }
                    
                }

                if(PerformList[0].Recon == "Player")
                {
                    Debug.Log("Player is here to perfom");
                }
                battleStates = PerformAction.PERFORMACTION;

            break;
            case(PerformAction.PERFORMACTION):
                //idle
            break;
            case(PerformAction.CHECKALIVE):
                if(PlayersInBattle.Count < 1)
                {
                    battleStates = PerformAction.LOSE;
                }
                else if(EnemiesInBattle.Count < 1)
                {
                    battleStates = PerformAction.WIN;
                }
                else
                {
                    clearAttackPanel();
                    PlayerInput = PlayerGUI.ACTIVATE;
                }

            break;
            case(PerformAction.LOSE):
            {
                Debug.Log("You lost!");
            }

            break;
            case(PerformAction.WIN):
            {
                Debug.Log("You win!");
                for (int i = 0; i < PlayersInBattle.Count; i++)
                {
                    PlayersInBattle[i].GetComponent<PlayerStateMachine>().currentState = PlayerStateMachine.TurnState.WAITING;
                }
            }

            break;
        }

        switch (PlayerInput)
        {
            case(PlayerGUI.ACTIVATE):
                if(PlayersToManage > 0)
                {
                    PlayersToManage [0].transform.FindChild ("Selector").gameObject.SetActive(true);
                    PlayerChoice = new HandleTurns();

                    AttackPanel.SetActive(true);

                    CreateAttackButtons();

                    PlayerInput = PlayerGUI.WAITING;
                }

            break;
            case(PlayerGUI.WAITING):
                //idle
            
            break;
            case(PlayerGUI.DONE):
                PlayerInputDone();

            break;
        }
    }

    public void CollectActions(HandleTurns input)
    {
        PerformList.Add(input);
    }

    public void EnemyButtons()
    {
        foreach(GameObject enemyBtn in enemyBtns )
        {
            Destroy(enemyBtn);
        }
        enemyBtns.Clear();

        foreach(GameObject enemy in EnemiesInBattle)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyStateMachine cur_enemy = enemy.GetComponent<EnemyStateMachine>();

            Text buttonText = newButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
            buttonText.text = cur_enemy.enemy.theName;

            button.EnemyPrefab = enemy;

            newButton.transform.SetParent(Spacer,false);

            enemyBtns.Add(newButton);
        }
    }

    public void Input1() //attack button
    {
        PlayerChoice.Attacker = PlayersToManage[0].theName;
        PlayerChoice.AttackersGameObject = PlayersToManage[0];
        PlayerChoice.Recon = "Player";
        PlayerChoice.chosenAttack = PlayersToManage[0].GetComponent<PlayerStateMachine>().player.attacks[0];
        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
    } 

    public void Input2(GameObject chosenEnemy) //enemy selection
    {
        PlayerChoice.AttackersTarget = chosenEnemy;
        PlayerInput = PlayerGUI.DONE;
    }

    void PlayerInputDone()
    {
        PerformList.Add(PlayerChoice);
        clearAttackPanel();

        PlayersToManage[0].transform.FindChild("Selector").gameObject.SetActive(false);
        PlayersToManage.RemoveAt(0);
        PlayerInput = PlayerGUI.ACTIVATE;
    }

    void clearAttackPanel()
    {
        EnemySelectPanel.SetActive(false);
        AttackPanel.SetActive(false);
        SkillPanel.SetActive(false);

        foreach(GameObject atkBtn in atkBtns)
        {
            Destroy(atkBtn);
        }
        atkBtns.Clear();
    }

    void CreateAttackButtons()
    {
        GameObject AttackButton = Instantiate(actionButton) as GameObject;
        Text AttackButtonText = AttackButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
        AttackButtonText.text = "Attack";
        AttackButton.GetComponent<Button>().onClick.AddListener(() => Input1());
        AttackButton.transform.SetParent(actionSpacer, false);
        atkBtns.Add(AttackButton);

        GameObject SkillAttackButton = Instantiate(actionButton) as GameObject;
        Text SkillAttackButtonText = SkillAttackButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
        SkillAttackButtonText.text = "Skill";
        SkillAttackButton.GetComponent<Button>().onClick.AddListener(() => Input3());
        SkillAttackButton.transform.SetParent(actionSpacer, false);
        atkBtns.Add(SkillAttackButton);

        if(PlayersToManage[0].GetComponent<PlayerStateMachine>().player.SkillAttacks.Count > 0)
        {
            foreach(BaseAttack skillAtk in PlayersToManage[0].GetComponent<PlayerStateMachine>().player.SkillAttacks)
            {
                GameObject SkillButton = Instantiate(skillButton) as GameObject;
                Text SkillButtonText = SkillButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
                SkillButtonText.text = skillAtk.attackName;
                AttackButton ATB = SkillButton.GetComponent<AttackButton>();
                ATB.skillAttackToPerform = skillAtk;
                SkillButton.transform.SetParent(skillSpacer, false);
                atkBtns.Add(SkillButton);
            }
        }
        else
        {
            SkillAttackButton.GetComponent<Button>().interactable = false;
        }
    }

    public void Input4(BaseAttack chosenSkill) // Choose skill
    {
        PlayerChoice.Attacker = PlayersToManage[0].theName;
        PlayerChoice.AttackersGameObject = PlayersToManage[0];
        PlayerChoice.Recon = "Player";

        PlayerChoice.chosenAttack = chosenSkill;
        SkillPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
    }

    public void Input3() //switching to skills
    {
        AttackPanel.SetActive(false);
        SkillPanel.SetActive(true);
    }
}
