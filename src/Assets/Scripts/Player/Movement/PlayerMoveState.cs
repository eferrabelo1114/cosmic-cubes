using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerBaseState transitionState;
    public Vector2 moveDirection;
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask collisionLayer;
    public Animator anim;
    bool canMove = true;
    public PlayerIndexController faces;
    public PlayerStatsController stats;
    // Start is called before the first frame update
    public override void EnterState()
    {
        
    }

    // Update is called once per frame
    public override PlayerBaseState UpdateState()
    {
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                AudioManager.instance.PlaySound("move");
                anim.SetBool("isMoving", true);
                if (Input.GetAxisRaw("Horizontal") == 1f)
                {
                    anim.SetBool("MoveRight", true);
                }
                else
                {
                    anim.SetBool("MoveLeft", true);
                }
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, collisionLayer))
                {
                    faces.MoveHorizontal((int)Mathf.Sign(Input.GetAxisRaw("Horizontal")));
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
                else
                {
                    Collider2D collider = Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, collisionLayer);

                    if (collider.gameObject.GetComponent<PushableBox>() != null)
                    {
                        collider.gameObject.GetComponent<PushableBox>().PushDie(true, Input.GetAxisRaw("Horizontal"), new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f));
                    }
                }
                faces.setFaces();
                //ExitState();
                return transitionState;
            }

            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                AudioManager.instance.PlaySound("move");
                if (Input.GetAxisRaw("Vertical") == 1f)
                {
                    anim.SetBool("MoveUp", true);
                }
                else
                {
                    anim.SetBool("MoveDown", true);
                }

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, collisionLayer))
                {
                    faces.MoveVertically((int)Mathf.Sign(Input.GetAxisRaw("Vertical")));
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
                else
                {
                    Collider2D collider = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, collisionLayer);

                    if (collider.gameObject.GetComponent<PushableBox>() != null)
                    {
                        collider.gameObject.GetComponent<PushableBox>().PushDie(false, Input.GetAxisRaw("Vertical"), new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f));
                    }
                }
                faces.setFaces();
                //ExitState();
                return transitionState;
            }
            return this;
        }
        return transitionState;
    }

    public override void ExitState(){
        anim.SetBool("MoveRight", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveUp", false);
        anim.SetBool("MoveDown", false);
        anim.SetBool("isMoving", false);
    }

    public override void PrintState(){
        Debug.Log("Move");
    }
}
