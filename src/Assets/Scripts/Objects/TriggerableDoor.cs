using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableDoor : Triggerable
{

    public LayerMask collisionLayer;
    public LayerMask objectLayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void trigger()
    {
        Debug.Log("TRIG: " + gameObject.layer + "OBJ: " + objectLayer);
        gameObject.layer = LayerMask.NameToLayer("Objects");
    }

    override public void unTrigger()
    {
        Debug.Log("UNTRIG: " + gameObject.layer);
        gameObject.layer = LayerMask.NameToLayer("Collision"); ;
    }
}
