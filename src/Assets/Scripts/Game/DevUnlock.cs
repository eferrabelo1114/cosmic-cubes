using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DevUnlock : MonoBehaviour
{   
    private string unlockSequence = "rnsm";
    private string currentSequence = "";
    private bool devSequenceCompleted = false;

    private float currentTime = 0f;

    private void unlockAll() {
        if (currentSequence == unlockSequence) {
            Debug.Log("DevUnlock unlocked");
            devSequenceCompleted = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (devSequenceCompleted) { return; }

        if (currentSequence == unlockSequence) {
            PlayerPrefs.SetInt("WorldReached", 2);
            PlayerPrefs.SetInt("LevelReached", 3);

            devSequenceCompleted = true;
        }

        if (currentTime >= 5) {
            unlockAll();
            currentSequence = "";
            currentTime = 0f;
        }

        if (Input.GetKeyDown("r"))
        {
            currentSequence += 'r';
        } 

        if (Input.GetKeyDown("n"))
        {
            currentSequence += 'n';
        } 

        if (Input.GetKeyDown("s"))
        {
            currentSequence += 's';
        } 

        if (Input.GetKeyDown("m"))
        {
            currentSequence += 'm';
        } 


        currentTime += Time.deltaTime;
    }
}
