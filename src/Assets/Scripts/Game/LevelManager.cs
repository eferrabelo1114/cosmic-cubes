using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;
    private GameObject LevelSpawnpoint;
    private CheckFace endOfLevel; 
    private LevelLoader levelLoader;

    private bool leveCompleted = false;

    public string NextLevel = "1-2";
    public Transform LevelLoader;

    void Awake() {
        levelLoader = Instantiate(LevelLoader).GetComponent<LevelLoader>();
    }

    void Start()
    {
        LevelSpawnpoint = GameObject.Find("PlayerSpawn");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        endOfLevel = GameObject.Find("End").GetComponent<CheckFace>();

        gameManager.currentPlayerSpawnpoint = LevelSpawnpoint.transform.position;

        Destroy(LevelSpawnpoint);

        SceneHelper.LoadScene("Player", true);
    }

    void Update() {
        if (!endOfLevel.goalMet) { return;}


        if (!leveCompleted) {
            Debug.Log("Level Completed");
            levelLoader.LoadLevel(NextLevel);
            leveCompleted = true;
        }
    }
}
