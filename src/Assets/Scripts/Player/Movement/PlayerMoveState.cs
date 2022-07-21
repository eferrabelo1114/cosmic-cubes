using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerBaseState transitionState;
    public Vector2 moveDirection;
    public PlayerStatsController stats;
    public LayerMask collisionLayer;
    // Start is called before the first frame update
    public override void EnterState()
    {
        
    }

    // Update is called once per frame
    public override PlayerBaseState UpdateState()
    {
        if (Vector3.Distance(transform.position, stats.movePoint.position) <= .05f)
        {

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                AudioManager.instance.PlaySound("move");
                stats.anim.SetBool("isMoving", true);
                if (Input.GetAxisRaw("Horizontal") == 1f)
                {
                    stats.anim.SetBool("MoveRight", true);
                }
                else
                {
                    stats.anim.SetBool("MoveLeft", true);
                }
                if (!Physics2D.OverlapCircle(stats.movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, collisionLayer))
                {
                    stats.faces.MoveHorizontal((int)Mathf.Sign(Input.GetAxisRaw("Horizontal")));
                    stats.movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
                else
                {
                    Collider2D collider = Physics2D.OverlapCircle(stats.movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, collisionLayer);

                    if (collider.gameObject.GetComponent<PushableBox>() != null)
                    {
                        collider.gameObject.GetComponent<PushableBox>().PushDie(true, Input.GetAxisRaw("Horizontal"), new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f));
                    }
                }
                stats.faces.setFaces();
                //ExitState();
                return transitionState;
            }

            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                AudioManager.instance.PlaySound("move");
                if (Input.GetAxisRaw("Vertical") == 1f)
                {
                    stats.anim.SetBool("MoveUp", true);
                }
                else
                {
                    stats.anim.SetBool("MoveDown", true);
                }

                if (!Physics2D.OverlapCircle(stats.movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, collisionLayer))
                {
                    stats.faces.MoveVertically((int)Mathf.Sign(Input.GetAxisRaw("Vertical")));
                    stats.movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
                else
                {
                    Collider2D collider = Physics2D.OverlapCircle(stats.movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, collisionLayer);

                    if (collider.gameObject.GetComponent<PushableBox>() != null)
                    {
                        collider.gameObject.GetComponent<PushableBox>().PushDie(false, Input.GetAxisRaw("Vertical"), new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f));
                    }
                }
                stats.faces.setFaces();
                //ExitState();
                return transitionState;
            }
            return this;
        }
        return transitionState;
    }

    public override void ExitState(){
    }

    public override string GetState(){
        return "Move";
    }
}
