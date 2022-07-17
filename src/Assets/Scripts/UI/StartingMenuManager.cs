using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartingMenuManager : MonoBehaviour
{
    private GameManager gameManager;
    private Animator Transition;
    private LevelLoader levelLoader;

    public AudioClip testSFXClip;
    public AudioClip testMusicClip;

    public GameObject GameManagerPrefab;
    public GameObject AudioManagerPrefab;

    public Transform LevelManager;

    public Button StartGameButton;
    public Button LevelButton;
    public Button OptionsButton;

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

        // For level testing in case you forget to put audio manager in scene
        GameObject AudioManagerObject = GameObject.FindGameObjectWithTag("AudioManager");
        if (AudioManagerObject == null)
        {
           Instantiate(AudioManagerPrefab);
        }
    }
    void Start()
    {
        LevelManager = Instantiate(LevelManager);
        levelLoader = LevelManager.GetComponent<LevelLoader>();

        StartGameButton.onClick.AddListener(StartGame);
        LevelButton.onClick.AddListener(LevelSelect);

        OptionsButton.onClick.AddListener(() => {
            SceneHelper.LoadScene("OptionsMenu", false);
        });

        AudioManager.instance.PlayMusic("2loop");
    }

    void LevelSelect() {
        SceneHelper.LoadScene("WorldSelect", false);
    }

    void StartGame()
    {
        int worldReached = PlayerPrefs.GetInt("WorldReached");
        int levelReached = PlayerPrefs.GetInt("LevelReached");
        
        if (worldReached == null) {
            PlayerPrefs.SetInt("WorldReached", 0);
            worldReached = 0;
        }

        if (levelReached == null || levelReached == 0) {
            PlayerPrefs.SetInt("LevelReached", 1);
            levelReached = 1;
        }

        string level = worldReached + "-" + levelReached;
        
        levelLoader.LoadLevel(level);
    }

}
