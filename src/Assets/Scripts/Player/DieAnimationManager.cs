using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAnimationManager : MonoBehaviour
{
    public List<Sprite> faceList;
    // Start is called before the first frame update

    // Update is called once per frame
    public void setFace(int index)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = faceList[index];
    }
}
