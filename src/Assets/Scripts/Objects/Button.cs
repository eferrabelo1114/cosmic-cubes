using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public CheckFace checkFace;
    public List<GameObject> objectList;
    public bool triggered = false;
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
        if (!triggered && checkFace.goalMet)
        {
            TriggerObjects();
            triggered = true;
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
}
