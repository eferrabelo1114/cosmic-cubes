using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour
{
    private LevelManager levelManager;
    public Text currentlevel;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        currentlevel.text = "Level: " + levelManager.level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
