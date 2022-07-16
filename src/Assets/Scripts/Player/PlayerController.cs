using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask collisionLayer;
    public Animator anim;
    bool canMove = true;
    float delay = .5f;

    public int[] verticalDiceReel = { 1, 5, 6 };
    public int[] horizontalDiceReel = { 2, 3, 5, 6 };

    public int verticalFaceIndex = 1;
    public int horizontalFaceIndex = 2;
    public int currentFace;
    public int topFace;
    public int botFace;
    public int rightFace;
    public int leftFace;
    public List<Sprite> indicators;
    public GameObject right;
    public GameObject left;
    public GameObject up;
    public GameObject down;


    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.position = gameManager.currentPlayerSpawnpoint;

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
                    MoveHorizontal((int)Mathf.Sign(Input.GetAxisRaw("Horizontal")));
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
                    MoveVertically((int)Mathf.Sign(Input.GetAxisRaw("Vertical")));
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
                StartCoroutine(MoveDelay());
            }
            currentFace = horizontalDiceReel[horizontalFaceIndex];
            anim.SetInteger("CurrentFace",currentFace);
            topFace = (verticalFaceIndex - 1 < 0 ? verticalDiceReel[verticalDiceReel.Length - 1] : verticalDiceReel[verticalFaceIndex - 1]);
            botFace = (verticalFaceIndex + 1 > verticalDiceReel.Length - 1 ? verticalDiceReel[0] : verticalDiceReel[verticalFaceIndex + 1]);
            leftFace = (horizontalFaceIndex - 1 < 0 ? horizontalDiceReel[horizontalDiceReel.Length - 1] : horizontalDiceReel[horizontalFaceIndex - 1]);
            rightFace = (horizontalFaceIndex + 1 > horizontalDiceReel.Length - 1 ? horizontalDiceReel[0] : horizontalDiceReel[horizontalFaceIndex + 1]);
            right.GetComponent<SpriteRenderer>().sprite = indicators[leftFace-1];
            left.GetComponent<SpriteRenderer>().sprite = indicators[rightFace-1];
            up.GetComponent<SpriteRenderer>().sprite = indicators[botFace-1];
            down.GetComponent<SpriteRenderer>().sprite = indicators[topFace-1];
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

    void MoveVertically(int dir)
    {
        if (dir < 0)
        {
            if (verticalFaceIndex - 1 < 0)
            {
                verticalFaceIndex = verticalDiceReel.Length - 1;
            }
            else
            {
                verticalFaceIndex -= 1;
            }

        }
        else
        {
            if (verticalFaceIndex + 1 > verticalDiceReel.Length - 1)
            {
                verticalFaceIndex = 0;
            }
            else
            {
                verticalFaceIndex += 1;
            }
        }
        horizontalDiceReel[horizontalFaceIndex] = verticalDiceReel[verticalFaceIndex];
    }

    void MoveHorizontal(int dir)
    {

        if (dir > 0)
        {
            if (horizontalFaceIndex - 1 < 0)
            {
                horizontalFaceIndex = horizontalDiceReel.Length - 1;
            }
            else
            {
                horizontalFaceIndex -= 1;
            }

        }
        else
        {
            if (horizontalFaceIndex + 1 > horizontalDiceReel.Length - 1)
            {
                horizontalFaceIndex = 0;
            }
            else
            {
                horizontalFaceIndex += 1;
            }
        }

        verticalDiceReel[verticalFaceIndex] = horizontalDiceReel[horizontalFaceIndex];
    }

    public int getCurrentFace()
    {
        return this.currentFace;
    }
}
