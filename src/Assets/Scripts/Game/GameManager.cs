using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float currentTimeScale;
    private bool gamePaused = false;

    public string currentLevel = "0-1";

    public Vector3 currentPlayerSpawnpoint;

    public int selectedWorld = 0;
    public int selectedLevel = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTimeScale = Time.timeScale;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UnpauseGame() {
        gamePaused = false; 
        Time.timeScale = currentTimeScale;
    }

    // Initialize Game
    public void ChangeLevel(string level) {
        int worldToChangeTo = (int)char.GetNumericValue(level[0]);
        int levelToChangeTo = (int)char.GetNumericValue(level[2]);

        currentLevel = level;
        SceneHelper.LoadScene(level, false);

        float worldReached = PlayerPrefs.GetInt("WorldReached");
        float levelReached = PlayerPrefs.GetInt("LevelReached");

        if (worldToChangeTo > worldReached) {
            PlayerPrefs.SetInt("WorldReached", worldToChangeTo);
            PlayerPrefs.SetInt("LevelReached", 1);
        } else if(worldToChangeTo == worldReached && levelToChangeTo > levelReached) {
            PlayerPrefs.SetInt("LevelReached", levelToChangeTo);
        }
    }

    public void RestartLevel() {
        LevelManager levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        if (currentLevel != levelManager.level) {
            currentLevel = levelManager.level;
        }

        SceneHelper.LoadScene(currentLevel, false);

        if (gamePaused) { UnpauseGame(); }
    }

    public void PauseGame(bool pauseGame) {
        if (pauseGame && !gamePaused) {
            gamePaused = true;
            Time.timeScale = 0;
            SceneHelper.LoadScene("PauseMenu", true);
            return;
        }
        
        gamePaused = false;
        Time.timeScale = currentTimeScale;
        SceneHelper.UnloadScene("PauseMenu");
    }

    public void ReturnToMenu() {
        if (gamePaused) { UnpauseGame(); }
        
        SceneHelper.LoadScene("StartingMenu", false);
    }
}
