using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private GameManager gameManager;
    
    public int level = 0;

    public Text buttonText;
    public Image buttonImage;
    public Button button;

    public Color worldReachedColor;
    public Color notReachedColor;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        buttonText.text = gameManager.selectedWorld + "-" + level;

        int levelReached = PlayerPrefs.GetInt("LevelReached");

        if (levelReached < level) {
            button.enabled = false;
            buttonImage.enabled = false;
            buttonText.color = notReachedColor;
        } else {
            button.enabled = true;
            buttonImage.enabled = true;
        }
    }
}
