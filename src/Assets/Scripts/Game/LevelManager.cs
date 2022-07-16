using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameObject player;

    public Transform LevelLoader;

    void Awake() {
        Instantiate(LevelLoader);

    }

    // Start is called before the first frame update
    void Start()
    {
        SceneHelper.LoadScene("Player", true);
        player = GameObject.Find("Player");
    }
}
