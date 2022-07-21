using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBlock : Triggerable
{
    public CheckFace checkFace;
    public bool isTriggered = false;
    public bool slide = false;
    void Update()
    {
        if (isTriggered)
        {
            if (checkFace.closestInteractable && checkFace.closestInteractable.GetComponent<PlayerStatsController>())
            {
                checkFace.closestInteractable.GetComponent<PlayerStatsController>().isSliding = true;
                //checkFace.closestInteractable.GetComponent<PlayerController>().Slide();
            }
            else if (checkFace.closestInteractable && checkFace.closestInteractable.GetComponent<PushableBox>())
            {
                checkFace.closestInteractable.GetComponent<PushableBox>().isSliding = true;

            }

        }
    }

    override public void trigger()
    {

        if (!isTriggered && checkFace.closestInteractable && checkFace.closestInteractable.GetComponent<PlayerStatsController>())
        {
            if (checkFace.closestInteractable.GetComponent<PlayerStatsController>().isSliding == false)
            {
                checkFace.closestInteractable.GetComponent<PlayerStatsController>().isSliding = true;
                //checkFace.closestInteractable.GetComponent<PlayerController>().Slide();
                isTriggered = true;
            }



        }
        else if (!isTriggered && checkFace.closestInteractable && checkFace.closestInteractable.GetComponent<PushableBox>())
        {
            if (checkFace.closestInteractable.GetComponent<PushableBox>().isSliding == false)
            {
                checkFace.closestInteractable.GetComponent<PushableBox>().isSliding = true;
                checkFace.closestInteractable.GetComponent<PushableBox>().canMove = false;
                isTriggered = true;
            }
        }

    }
    override public void unTrigger()
    {
        // if (slide)
        isTriggered = false;

        if (checkFace.closestInteractable && checkFace.closestInteractable.GetComponent<PlayerStatsController>())
        {
            checkFace.closestInteractable.GetComponent<PlayerController>().isSliding = false;
        }

        if (checkFace.closestInteractable && checkFace.closestInteractable.GetComponent<PushableBox>())
        {
            checkFace.closestInteractable.GetComponent<PushableBox>().isSliding = false;

        }

    }
}

// Update is called once per frame

