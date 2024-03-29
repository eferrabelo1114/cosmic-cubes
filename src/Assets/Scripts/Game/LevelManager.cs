using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;
    private GameObject LevelSpawnpoint;
    private CheckFace endFaceChecker;
    private LevelLoader levelLoader;
    private bool leveCompleted = false;

    [Header("Level Settings")]
    public StartingFaceConfig startingFace;
    public string level = "0-0";
    public string NextLevel = "1-2";

    [Header("Dont change these")]
    public GameObject AudioManagerPrefab;
    public GameObject LevelLoader;
    public GameObject GameManagerPrefab;

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

        // For level testing in case you forget to remove camera
        GameObject camera = GameObject.Find("Main Camera");
        if (camera != null)
        {
           Destroy(camera);
        }

        // For level testing in case you forget to remove event system
        GameObject eventSystem = GameObject.Find("EventSystem");
        if (eventSystem != null)
        {
           Destroy(eventSystem);
        }
    }

    void Start()
    {
        // Gets the level loader for the scene
        levelLoader = Instantiate(LevelLoader).GetComponent<LevelLoader>();

        // Obtains the spawn point for the level
        GameObject LevelSpawnpoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        if (LevelSpawnpoint == null)
        {
            Debug.LogWarning("Level Spawnpoint Missing in Scene");
            return;
        }

        // Obtains the end of level for the level
        GameObject EndOfLevelObject = GameObject.FindGameObjectWithTag("EndOfLevel");
        if (EndOfLevelObject == null)
        {
            Debug.LogWarning("Level End Point Missing in Scene");
            return;
        }

        // Make sure StartingFaces is set
        if (startingFace == null)
        {
            Debug.LogWarning("Level has no starting face!");
            return;
        }

        // Obtains the components
        endFaceChecker = EndOfLevelObject.GetComponent<CheckFace>();

        // TODO: Fucking remove all of this, the scene can get the spawnpoint this dumb
        gameManager.currentPlayerSpawnpoint = LevelSpawnpoint.transform.position;
        Destroy(LevelSpawnpoint);

        // Load the Level's UI
        SceneHelper.LoadScene("LevelUI", true);

        // Finally spawn the player in
        SceneHelper.LoadScene("Player", true);

        // Make end point invisible
        EndOfLevelObject.GetComponent<SpriteRenderer>().enabled = false;

        CallAfterDelay.Create(0, () =>
        {
            GameObject.Find("Player").GetComponent<PlayerController>().loadFaces(startingFace.verticalDiceReel, startingFace.horizontalDiceReel);
        });

        string AudioFile = "World" + level[0];
        AudioManager.instance.PlayMusic(AudioFile, true, 0.25f, true);
    }

    void Update()
    {
        // Check for a pause game
        if (Input.GetKeyDown("escape") && gameManager != null)
        {
            gameManager.PauseGame(true);
        }

        // Restart Level
        if (Input.GetKeyDown("r") && gameManager != null)
        {
            gameManager.RestartLevel();
        }

        // If the level has not been complete dont do anything
        if (!endFaceChecker.goalMet) { return; }

        // Level is completed, load next level
        if (!leveCompleted)
        {
            if (level == "2-3") {
                SceneHelper.LoadScene("StartingMenu", false);
                leveCompleted = true;
                AudioManager.instance.PlaySound("exit!");
            } else {
                levelLoader.LoadLevel(NextLevel);
                leveCompleted = true;
                AudioManager.instance.PlaySound("exit!");
            }
        }
    }
}
