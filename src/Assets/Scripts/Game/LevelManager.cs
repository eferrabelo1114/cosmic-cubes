using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;
    private GameObject LevelSpawnpoint;

    public Transform LevelLoader;

    void Awake() {
        Instantiate(LevelLoader);

    }

    // Start is called before the first frame update
    void Start()
    {
        LevelSpawnpoint = GameObject.Find("PlayerSpawn");

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        gameManager.currentPlayerSpawnpoint = LevelSpawnpoint.transform.position;

        Destroy(LevelSpawnpoint);

        SceneHelper.LoadScene("Player", true);
    }
}
