using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFace : MonoBehaviour
{

    // private float count = 0;
    public int refreshRate = 10;
    public float interactionRadius = .1f;
    public string interactableTag = "Dice";

    public int goalFace;
    public bool goalMet = false;
    public GameObject closestInteractable;
    private float count = 0;
    public bool playerOnly = false;
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
            if (closestObj && closestObj.GetComponent<PlayerController>())
            {
                if (!closestObj.GetComponent<PlayerController>().isMoving)
                {
                    goalMet = goalFace == closestObj.GetComponent<PlayerController>().getCurrentFace() || goalFace == 0;
                    closestInteractable = closestObj;
                }
                else
                {
                    closestInteractable = closestObj;
                    goalMet = false;
                }

            }
            else if (!playerOnly && closestObj && closestObj.GetComponent<PushableBox>())
            {
                if (!closestObj.GetComponent<PushableBox>().isMoving)
                {
                    goalMet = goalFace == closestObj.GetComponent<PushableBox>().getCurrentFace() || goalFace == 0;
                    closestInteractable = closestObj;
                }
                else
                {
                    closestInteractable = closestObj;
                    goalMet = false;
                }
            }
            else
            {
                closestInteractable = closestObj;
                goalMet = false;
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

