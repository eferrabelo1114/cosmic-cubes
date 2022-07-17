using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    private GameManager gameManager;

    public Button button1;
    public Button button2;
    public Button button3;

    public Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        button1.onClick.AddListener(() => {
            SelectLevel(1);
        });

        button2.onClick.AddListener(() => {
            SelectLevel(2);
        });

        button3.onClick.AddListener(() => {
            SelectLevel(3);
        });

        backButton.onClick.AddListener(Back);
    }


    void Back() {
        SceneHelper.LoadScene("WorldSelect", false);
    }

    void SelectLevel(int levelNum) {
        gameManager.selectedLevel = levelNum;

        string level = gameManager.selectedWorld + "-" + levelNum;

        gameManager.ChangeLevel(level);
    }
}
