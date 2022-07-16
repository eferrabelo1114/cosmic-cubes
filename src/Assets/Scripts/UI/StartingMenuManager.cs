using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartingMenuManager : MonoBehaviour
{
    private GameManager gameManager;
    private Animator Transition;
    private LevelLoader levelLoader;

    public Transform LevelManager;
    public Button StartGameButton;

    void Start() {
        LevelManager = Instantiate(LevelManager);
        levelLoader = LevelManager.GetComponent<LevelLoader>();

        StartGameButton.onClick.AddListener(StartGame);
    }

    void StartGame() {
        levelLoader.LoadLevel("1-1");
    }

}
