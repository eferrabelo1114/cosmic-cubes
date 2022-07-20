using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransition : PlayerBaseState
{
    public PlayerBaseState slideState;
    public PlayerBaseState moveState;
    public PlayerStatsController stats;
    bool canTransition = false;
    bool hasRun = false;


    public override void EnterState(){
        canTransition = false;
    }

    public override PlayerBaseState UpdateState(){
        if(!this.canTransition && !this.hasRun){
            Debug.Log("Starting");
            StartCoroutine(CanTransition());
        }
        else if(canTransition){
            if(stats.isSliding){
                return slideState;
            }
            else{
                Debug.Log("transitioning to move");
                return moveState;
            }
        }
        return this;

    }
    public override void ExitState(){

    }

        private IEnumerator CanTransition()
    {
        Debug.Log("Delaying");
        this.canTransition = false;
        this.hasRun = true;
        yield return new WaitForSeconds(1);
        Debug.Log("CanMove");
        this.canTransition = true;
        this.hasRun = false;
    }

    public override void PrintState(){
        Debug.Log("Transition");
    }
}
