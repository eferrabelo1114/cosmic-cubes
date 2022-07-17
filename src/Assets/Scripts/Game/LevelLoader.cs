using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private GameManager gameManager;
    public Animator Transition;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void LoadLevel(string level)
    {
        StartCoroutine(ChangeLevel(level));
    }

    IEnumerator ChangeLevel(string level)
    {
        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        gameManager.ChangeLevel(level);
    }
}
