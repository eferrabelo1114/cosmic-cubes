using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartingMenuManager : MonoBehaviour
{
    private GameManager gameManager;
    private Animator Transition;
    private LevelLoader levelLoader;

    public GameObject GameManagerPrefab;
    public Transform LevelManager;
    public Button StartGameButton;

    void Awake()
    {
        // For level testing in case you forget to put game manager in scene
        // A game manager object should always be found unless testing level
        GameObject GameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        if (GameManagerObject == null)
        {
            gameManager = Instantiate(GameManagerPrefab).GetComponent<GameManager>();
        }
        else
        {
            gameManager = GameManagerObject.GetComponent<GameManager>();
        }
    }

    void Start()
    {
        LevelManager = Instantiate(LevelManager);
        levelLoader = LevelManager.GetComponent<LevelLoader>();

        StartGameButton.onClick.AddListener(StartGame);

        SceneHelper.LoadScene("Music", true);
        AudioManager.PlaySound();
    }

    void StartGame()
    {
        levelLoader.LoadLevel("0-1");
    }

}
