using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float currentTimeScale;
    private bool gamePaused = false;

    public string currentLevel = null;
    public Vector3 currentPlayerSpawnpoint;
    
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
        SceneHelper.LoadScene(level, false);
        currentLevel = level;
    }

    public void RestartLevel() {
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
