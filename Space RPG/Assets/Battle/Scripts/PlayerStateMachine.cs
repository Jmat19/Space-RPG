using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
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

            break;
            case(TurnState.WAITING):

            break;
            case(TurnState.SELECTING):

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
        float calc_cooldown = cur_cooldown / max_cooldown;
        ProgressBar.transform.localScale = new Vector2(Mathf.Clamp(calc_cooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);

        if (cur_cooldown >= max_cooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }
}