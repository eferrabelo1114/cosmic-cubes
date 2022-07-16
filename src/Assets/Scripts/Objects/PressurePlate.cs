using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public CheckFace checkFace;
    public List<GameObject> objectList;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnCollisionEnter2D()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (checkFace.goalMet)
        {
            TriggerObjects();
        }
        else
        {
            UnTriggerObjects();
        }
    }

    void TriggerObjects()
    {
        foreach (GameObject gameObject in objectList)
        {
            Debug.Log(gameObject.GetComponent<Triggerable>());
            gameObject.GetComponent<Triggerable>().trigger();
        }
    }

    void UnTriggerObjects()
    {
        foreach (GameObject gameObject in objectList)
        {
            gameObject.GetComponent<Triggerable>().unTrigger();
        }
    }
}
