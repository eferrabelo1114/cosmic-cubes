using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndexController : MonoBehaviour
{

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
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        if (faceConfig != null)
        {
            loadFaces(faceConfig.verticalDiceReel, faceConfig.horizontalDiceReel);
        }
        
    }

    // Update is called once per frame
    public void setFaces(){
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


    public void MoveVertically(int dir)
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

    public void MoveHorizontal(int dir)
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
