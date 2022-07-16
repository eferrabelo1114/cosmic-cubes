using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    private GameManager gameManager;

    public Button resumButton;
    public Button restartButton;
    public Button returnToMenu;

    public Text currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        resumButton.onClick.AddListener(UnpauseGame);
        restartButton.onClick.AddListener(gameManager.RestartLevel);
        returnToMenu.onClick.AddListener(gameManager.ReturnToMenu);

        currentLevel.text = gameManager.currentLevel;
    }

    void UnpauseGame() {
        gameManager.PauseGame(false);
    }
}
