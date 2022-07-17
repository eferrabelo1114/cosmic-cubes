using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBox : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask collisionLayer;
    public Animator anim;
    public bool canMove = true;
    public bool isMoving = false;
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
    public GameObject sfx;
    public StartingFaceConfig faceConfig;
    public bool isSliding = false;

    // Start is called before the first frame update
    void Start()
    {

        movePoint.parent = null;

        if (faceConfig != null)
        {
            loadFaces(faceConfig.verticalDiceReel, faceConfig.horizontalDiceReel);
        }

    }
    public void Slide(bool horizontal, float axis)
    {
        Vector3 dir = horizontal ? new Vector3(axis, 0f, 0f) : new Vector3(0f, axis, 0f);
        canMove = false;
        isMoving = true;
        while (!Physics2D.OverlapCircle(movePoint.position + dir, .2f, collisionLayer))
        {
            movePoint.position += dir;
        }

        canMove = true;
        isMoving = false;
    }
    
    public void PushDie(bool horizontal, float axis, Vector3 dir)
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f && canMove)
        {

            if (horizontal)
            {
                AudioManager.instance.PlaySound("move");
                anim.SetBool("isMoving", true);

                if (axis > 0)
                {
                    anim.SetBool("MoveRight", true);
                }
                else
                {
                    anim.SetBool("MoveLeft", true);
                }
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(axis, 0f, 0f), .2f, collisionLayer))
                {
                    MoveHorizontal((int)Mathf.Sign(axis));
                    movePoint.position += new Vector3(axis, 0f, 0f);
                }
                StartCoroutine(MoveDelay());
            }

            else if (!horizontal)
            {
                Debug.Log("Moving");
                AudioManager.instance.PlaySound("move");
                if (axis > 0)
                {
                    anim.SetBool("MoveUp", true);
                }
                else
                {
                    anim.SetBool("MoveDown", true);
                }

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, axis, 0f), .2f, collisionLayer))
                {
                    MoveVertically((int)Mathf.Sign(axis));
                    movePoint.position += new Vector3(0f, axis, 0f);
                }
                StartCoroutine(MoveDelay());
            }
            currentFace = horizontalDiceReel[horizontalFaceIndex];
            anim.SetInteger("CurrentFace", currentFace);
            topFace = (verticalFaceIndex - 1 < 0 ? verticalDiceReel[verticalDiceReel.Length - 1] : verticalDiceReel[verticalFaceIndex - 1]);
            botFace = (verticalFaceIndex + 1 > verticalDiceReel.Length - 1 ? verticalDiceReel[0] : verticalDiceReel[verticalFaceIndex + 1]);
            leftFace = (horizontalFaceIndex - 1 < 0 ? horizontalDiceReel[horizontalDiceReel.Length - 1] : horizontalDiceReel[horizontalFaceIndex - 1]);
            rightFace = (horizontalFaceIndex + 1 > horizontalDiceReel.Length - 1 ? horizontalDiceReel[0] : horizontalDiceReel[horizontalFaceIndex + 1]);
            right.GetComponent<SpriteRenderer>().sprite = indicators[leftFace - 1];
            left.GetComponent<SpriteRenderer>().sprite = indicators[rightFace - 1];
            up.GetComponent<SpriteRenderer>().sprite = indicators[botFace - 1];
            down.GetComponent<SpriteRenderer>().sprite = indicators[topFace - 1];
        }
        else if (isSliding)
        {
            Slide(horizontal, axis);
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (!(Vector3.Distance(transform.position, movePoint.position) <= .05f && canMove))
        {

            anim.SetBool("MoveRight", false);
            anim.SetBool("MoveLeft", false);
            anim.SetBool("MoveUp", false);
            anim.SetBool("MoveDown", false);
            anim.SetBool("isMoving", false);
        }
        else
        {
            currentFace = horizontalDiceReel[horizontalFaceIndex];
            anim.SetInteger("CurrentFace", currentFace);
            topFace = (verticalFaceIndex - 1 < 0 ? verticalDiceReel[verticalDiceReel.Length - 1] : verticalDiceReel[verticalFaceIndex - 1]);
            botFace = (verticalFaceIndex + 1 > verticalDiceReel.Length - 1 ? verticalDiceReel[0] : verticalDiceReel[verticalFaceIndex + 1]);
            leftFace = (horizontalFaceIndex - 1 < 0 ? horizontalDiceReel[horizontalDiceReel.Length - 1] : horizontalDiceReel[horizontalFaceIndex - 1]);
            rightFace = (horizontalFaceIndex + 1 > horizontalDiceReel.Length - 1 ? horizontalDiceReel[0] : horizontalDiceReel[horizontalFaceIndex + 1]);
            right.GetComponent<SpriteRenderer>().sprite = indicators[leftFace - 1];
            left.GetComponent<SpriteRenderer>().sprite = indicators[rightFace - 1];
            up.GetComponent<SpriteRenderer>().sprite = indicators[botFace - 1];
            down.GetComponent<SpriteRenderer>().sprite = indicators[topFace - 1];
        }

    }


    private IEnumerator MoveDelay()
    {
        Debug.Log("go");
        canMove = false;
        isMoving = true;
        yield return new WaitForSeconds(delay);
        canMove = true;
        isMoving = false;
    }

    void MoveVertically(int dir)
    {
        if (dir < 0)
        {
            int temp = verticalDiceReel[verticalDiceReel.Length - 1];

            for (int i = verticalDiceReel.Length - 1; i > 0; i--)
            {
                verticalDiceReel[i] = verticalDiceReel[i - 1];
            }

            verticalDiceReel[0] = horizontalDiceReel[0];
            horizontalDiceReel[0] = temp;
        }
        else
        {
            int temp = verticalDiceReel[0];

            for (int i = 0; i < verticalDiceReel.Length - 1; i++)
            {
                verticalDiceReel[i] = verticalDiceReel[i + 1];
            }

            verticalDiceReel[verticalDiceReel.Length - 1] = horizontalDiceReel[0];
            horizontalDiceReel[0] = temp;
        }


        horizontalDiceReel[horizontalFaceIndex] = verticalDiceReel[verticalFaceIndex];
    }

    void MoveHorizontal(int dir)
    {

        if (dir > 0)
        {
            int temp = horizontalDiceReel[horizontalDiceReel.Length - 1];

            for (int i = horizontalDiceReel.Length - 1; i > 0; i--)
            {
                horizontalDiceReel[i] = horizontalDiceReel[i - 1];
            }

            horizontalDiceReel[0] = temp;
        }
        else
        {
            int temp = horizontalDiceReel[0];

            for (int i = 0; i < horizontalDiceReel.Length - 1; i++)
            {
                horizontalDiceReel[i] = horizontalDiceReel[i + 1];
            }

            horizontalDiceReel[horizontalDiceReel.Length - 1] = temp;
        }

        verticalDiceReel[verticalFaceIndex] = horizontalDiceReel[horizontalFaceIndex];
    }
    public int getCurrentFace()
    {
        return this.currentFace;
    }

    public void loadFaces(int[] vertical, int[] horizontal)
    {
        this.verticalDiceReel = (int[])vertical.Clone();
        this.horizontalDiceReel = (int[])horizontal.Clone();
        this.verticalFaceIndex = 1;
        this.horizontalFaceIndex = 2;
    }
}
