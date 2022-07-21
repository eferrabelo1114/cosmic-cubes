using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerBaseState
{
    public PlayerBaseState transitionState;
    Vector3 dir;
    public PlayerStatsController stats;

    public override void EnterState(){
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        }

        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            dir = new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
        }
    }
    public override PlayerBaseState UpdateState(){
        while (!Physics2D.OverlapCircle(stats.movePoint.position + dir, .2f, stats.collisionLayer))
            {
                stats.movePoint.position += dir;
            }
            
        if(Vector3.Distance(transform.position, stats.movePoint.position) >= .001f){
            return this;
        }

        else{
            return transitionState;
        }
    }
    public override void ExitState(){
        dir = Vector3.zero;
        stats.isSliding = false;
    }
    public override string GetState(){
        return "Slide";

    }
}
