using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSelect : MonoBehaviour
{
    private GameManager gameManager;

    public Button world0Button;
    public Button world1Button;
    public Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        int worldReached = PlayerPrefs.GetInt("WorldReached");
        int levelReached = PlayerPrefs.GetInt("LevelReached");

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();


        world0Button.onClick.AddListener(() => {
            ChangeWorld(0);
        });


        world1Button.onClick.AddListener(() => {
            ChangeWorld(1);
        });

        backButton.onClick.AddListener(Back);
    }


    void Back() {
        SceneHelper.LoadScene("StartingMenu", false);
    }

    void ChangeWorld(int worldNum) {
        gameManager.selectedWorld = worldNum;
        SceneHelper.LoadScene("LevelSelect", false);
    }
}
