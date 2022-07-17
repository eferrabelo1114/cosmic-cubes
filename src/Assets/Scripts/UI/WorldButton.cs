using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldButton : MonoBehaviour
{

    public int world = 0;

    public Text buttonText;
    public Image buttonImage;
    public Button button;

    public Color worldReachedColor;
    public Color notReachedColor;

    // Start is called before the first frame update
    void Start()
    {
        int worldReached = PlayerPrefs.GetInt("WorldReached");

        if (worldReached < world) {
            button.enabled = false;
            buttonImage.enabled = false;
            buttonText.color = notReachedColor;
        } else {
            button.enabled = true;
            buttonImage.enabled = true;
        }
    }
}
