using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFace : MonoBehaviour
{

    // private float count = 0;
    public int refreshRate = 10;
    public float interactionRadius = 1;
    public string interactableTag = "Dice";

    public int goalFace;
    public bool goalMet = false;

    private float count = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        count += Time.fixedDeltaTime;
        GameObject closestObj = null;
        if (count > (float)(1 / refreshRate))
        {

            closestObj = FindClosestObj(interactableTag);
            Debug.Log(closestObj);
            if (closestObj && closestObj.GetComponent<PlayerController>())
            {
                Debug.Log(closestObj.GetComponent<PlayerController>().getCurrentFace());
                goalMet = goalFace == closestObj.GetComponent<PlayerController>().getCurrentFace();
            }
        }

    }

    public GameObject FindClosestObj(string type)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, interactionRadius);

        GameObject closestObj = null;

        float minDist = Mathf.Infinity;

        foreach (Collider2D collider2D in colliders)
        {

            if (collider2D.gameObject.tag.Equals(type))
            {

                Vector2 playerPosition = gameObject.transform.position;
                Vector2 objectPosition = collider2D.gameObject.transform.position;
                Debug.DrawLine(playerPosition, objectPosition);
                float offset = Vector2.Distance(playerPosition, objectPosition);

                if (offset < minDist)
                {

                    closestObj = collider2D.gameObject;
                    minDist = offset;

                }

            }
        }

        return closestObj;

    }

}

