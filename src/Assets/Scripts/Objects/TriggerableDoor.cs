using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableDoor : Triggerable
{

    public LayerMask collisionLayer;
    public LayerMask objectLayer;
    public Sprite openedDoor;
    public Sprite closedDoor;
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
        gameObject.layer = LayerMask.NameToLayer("Default");
        gameObject.GetComponent<SpriteRenderer>().sprite = openedDoor;
    }

    override public void unTrigger()
    {
        gameObject.layer = LayerMask.NameToLayer("Collision");
        gameObject.GetComponent<SpriteRenderer>().sprite = closedDoor;
    }
}
