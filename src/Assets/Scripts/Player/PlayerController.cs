using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask collisionLayer;
    public Animator anim;
    bool canMove = true;
    float delay = .5f;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        Debug.Log(canMove);
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f&&canMove)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                anim.SetBool("isMoving",true);

                if(Input.GetAxisRaw("Horizontal") == 1f){
                    anim.SetBool("MoveRight",true);
                }
                else
                {
                    anim.SetBool("MoveLeft",true);
                }
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, collisionLayer))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
                StartCoroutine(MoveDelay());
            }

            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if(Input.GetAxisRaw("Vertical") == 1f){
                    anim.SetBool("MoveUp",true);
                }
                else
                {
                    anim.SetBool("MoveDown",true);
                }

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, collisionLayer))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
                StartCoroutine(MoveDelay());
            }
    }
    else{
            anim.SetBool("MoveRight",false);
            anim.SetBool("MoveLeft",false);
            anim.SetBool("MoveUp",false);
            anim.SetBool("MoveDown",false);
            anim.SetBool("isMoving",false);
        
    }


    }


    private IEnumerator MoveDelay(){
        Debug.Log("go");
        canMove = false;
        yield return new WaitForSeconds(delay);
        canMove = true;
    }
}
