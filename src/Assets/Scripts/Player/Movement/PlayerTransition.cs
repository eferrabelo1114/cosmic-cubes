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
        if(!stats.isSliding){
        canTransition = false;
        }
        else{
        StartCoroutine(SlideTransition());
        }
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
                return moveState;
            }
        }
        return this;

    }
    public override void ExitState(){

    }

        private IEnumerator CanTransition()
    {
        this.canTransition = false;
        this.hasRun = true;
        stats.anim.SetBool("MoveRight", false);
        stats.anim.SetBool("MoveLeft", false);
        stats.anim.SetBool("MoveUp", false);
        stats.anim.SetBool("MoveDown", false);
        stats.anim.SetBool("isMoving", false);
        yield return new WaitForSeconds(stats.delay);
        this.canTransition = true;
        this.hasRun = false;
    }

    private IEnumerator SlideTransition()
    {
        this.canTransition = false;
        this.hasRun = true;
        stats.anim.SetBool("MoveRight", false);
        stats.anim.SetBool("MoveLeft", false);
        stats.anim.SetBool("MoveUp", false);
        stats.anim.SetBool("MoveDown", false);
        stats.anim.SetBool("isMoving", false);
        yield return new WaitForSeconds(.001f);
        this.canTransition = true;
        this.hasRun = false;
    }

    public override string GetState(){
        return "Transition";
    }
}
