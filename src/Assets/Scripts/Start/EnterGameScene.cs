using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGameScene : MonoBehaviour
{


public void StartGame(){
    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainGameScene");
}
}
