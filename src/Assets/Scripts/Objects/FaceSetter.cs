using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSetter : Triggerable
{
    public CheckFace checkFace;
    public StartingFaceConfig face;
    public bool isTriggered = false;

    override public void trigger()
    {

        if (!isTriggered && checkFace.closestInteractable && checkFace.closestInteractable.GetComponent<PlayerController>())
        {
            checkFace.closestInteractable.GetComponent<PlayerController>().loadFaces(face.verticalDiceReel, face.horizontalDiceReel);
            isTriggered = true;
        }

    }
    override public void unTrigger()
    {
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {


    }

}



