using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;
    private CheckFace endFaceChecker; 
    private LevelLoader levelLoader;

    private bool leveCompleted = false;
    
    public string NextLevel = "1-2";

    public Transform LevelLoader;
    public Transform GameManagerPrefab;

    void Awake() {
        // For level testing in case you forget to put game manager in scene
        // A game manager object should always be found unless testing level
        GameObject GameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        if (GameManagerObject == null) {
            gameManager = Instantiate(GameManagerPrefab).GetComponent<GameManager>();
        } else {
            gameManager = GameManagerObject.GetComponent<GameManager>();
        }
    }

    void Start()
    {
        // Gets the level loader for the scene
        levelLoader = Instantiate(LevelLoader).GetComponent<LevelLoader>();

        // Obtains the spawn point for the level
        GameObject LevelSpawnpoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        if (LevelSpawnpoint == null) {
            Debug.LogWarning("Level Spawnpoint Missing in Scene");
            return;
        }

        // Obtains the end of level for the level
        GameObject EndOfLevelObject = GameObject.FindGameObjectWithTag("EndOfLevel");
        if (EndOfLevelObject == null) {
            Debug.LogWarning("Level End Point Missing in Scene");
            return;
        }

        // Obtains the components
        endFaceChecker = EndOfLevelObject.GetComponent<CheckFace>();

        // TODO: Fucking remove all of this, the scene can get the spawnpoint this dumb
        gameManager.currentPlayerSpawnpoint = LevelSpawnpoint.transform.position;
        Destroy(LevelSpawnpoint);

        // Finally spawn the player in
        SceneHelper.LoadScene("Player", true);
    }

    void Update() {
        // If the level has not been complete dont do anything
        if (!endFaceChecker.goalMet) { return;}

        // Level is completed, load next level
        if (!leveCompleted) {
            Debug.Log("Level Completed");
            levelLoader.LoadLevel(NextLevel);
            leveCompleted = true;
        }
    }
}
