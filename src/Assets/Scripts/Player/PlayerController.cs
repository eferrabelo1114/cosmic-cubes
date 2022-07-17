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
    public bool isMoving = false;
    public bool isSliding = false;
    public int[] verticalDiceReel = { 1, 5, 6 };
    public int[] horizontalDiceReel = { 2, 3, 5, 6 };

    public int verticalFaceIndex = 1;
    public int horizontalFaceIndex = 2;
    public int horizontalBackIndex = 0;
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
    public StartingFaceConfig faceConfig;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        transform.position = gameManager.currentPlayerSpawnpoint;

        movePoint.parent = null;

        if (faceConfig != null)
        {
            loadFaces(faceConfig.verticalDiceReel, faceConfig.horizontalDiceReel);
        }
    }

    public void Slide()
    {
        // if (!isSliding)
        // {
        Vector3 dir;
        canMove = false;
        isMoving = true;
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            while (!Physics2D.OverlapCircle(movePoint.position + dir, .2f, collisionLayer))
            {
                movePoint.position += dir;
            }
        }

        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            dir = new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            while (!Physics2D.OverlapCircle(movePoint.position + dir, .2f, collisionLayer))
            {
                movePoint.position += dir;
            }

        }

        canMove = true;
        isMoving = false;
        // isSliding = false;

        // }

        // StartCoroutine(MoveDelay());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f && canMove)
        {

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                AudioManager.instance.PlaySound("move");
                anim.SetBool("isMoving", true);
                // isMoving = true;
                if (Input.GetAxisRaw("Horizontal") == 1f)
                {
                    anim.SetBool("MoveRight", true);
                }
                else
                {
                    anim.SetBool("MoveLeft", true);
                }
                // detector.position = movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal");
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, collisionLayer))
                {
                    MoveHorizontal((int)Mathf.Sign(Input.GetAxisRaw("Horizontal")));
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
                else
                {
                    Collider2D collider = Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, collisionLayer);

                    if (collider.gameObject.GetComponent<PushableBox>() != null)
                    {
                        Debug.Log("Pushed Horizontal");
                        collider.gameObject.GetComponent<PushableBox>().PushDie(true, Input.GetAxisRaw("Horizontal"), new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f));
                    }
                }
                StartCoroutine(MoveDelay());
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
                    MoveVertically((int)Mathf.Sign(Input.GetAxisRaw("Vertical")));
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
                else
                {
                    Collider2D collider = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, collisionLayer);

                    if (collider.gameObject.GetComponent<PushableBox>() != null)
                    {
                        Debug.Log("Pushed Vertical");
                        collider.gameObject.GetComponent<PushableBox>().PushDie(false, Input.GetAxisRaw("Vertical"), new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f));
                    }
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
        else
        {
            if (isSliding)
            {
                if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
                {
                    isSliding = false;
                }

            }
            anim.SetBool("MoveRight", false);
            anim.SetBool("MoveLeft", false);
            anim.SetBool("MoveUp", false);
            anim.SetBool("MoveDown", false);
            anim.SetBool("isMoving", false);
            // isMoving = false;
        }

    }

    private IEnumerator MoveDelay()
    {
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

    // void MoveVertically(int dir)
    // {
    //     if (dir < 0)
    //     {
    //         if (verticalFaceIndex - 1 < 0)
    //         {
    //             verticalFaceIndex = verticalDiceReel.Length - 1;
    //         }
    //         else
    //         {
    //             verticalFaceIndex -= 1;
    //         }

    //     }
    //     else
    //     {
    //         if (verticalFaceIndex + 1 > verticalDiceReel.Length - 1)
    //         {
    //             verticalFaceIndex = 0;
    //         }
    //         else
    //         {
    //             verticalFaceIndex += 1;
    //         }
    //     }

    //     horizontalDiceReel[horizontalFaceIndex] = verticalDiceReel[verticalFaceIndex];
    // }

    // void MoveHorizontal(int dir)
    // {

    //     if (dir > 0)
    //     {
    //         if (horizontalFaceIndex - 1 < 0)
    //         {
    //             horizontalFaceIndex = horizontalDiceReel.Length - 1;
    //         }
    //         else
    //         {
    //             horizontalFaceIndex -= 1;
    //         }

    //     }
    //     else
    //     {
    //         if (horizontalFaceIndex + 1 > horizontalDiceReel.Length - 1)
    //         {
    //             horizontalFaceIndex = 0;
    //         }
    //         else
    //         {
    //             horizontalFaceIndex += 1;
    //         }
    //     }

    //     verticalDiceReel[verticalFaceIndex] = horizontalDiceReel[horizontalFaceIndex];
    // }

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
