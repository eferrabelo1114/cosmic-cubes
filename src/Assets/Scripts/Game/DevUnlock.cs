using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DevUnlock : MonoBehaviour
{   
    private string unlockSequence = "rnsm";
    private string resetSequence = "reset";

    private string currentSequence = "";
    private float currentTime = 0f;

    private bool unlocked = false;
    private bool reset = false;
    // Update is called once per frame
    void Update()
    {
        if (currentSequence == unlockSequence && !unlocked) {
            PlayerPrefs.SetInt("WorldReached", 2);
            PlayerPrefs.SetInt("LevelReached", 3);
            AudioManager.instance.PlaySound("Button2");
            unlocked = true;

            if (reset) {
                reset = false;
            }
        }

        if (currentSequence == resetSequence && !reset) {
            PlayerPrefs.SetInt("WorldReached", 0);
            PlayerPrefs.SetInt("LevelReached", 1);
            AudioManager.instance.PlaySound("Button3");
            reset = true;

            if (unlocked) {
                unlocked = false;
            }
        }

        if (currentTime >= 5) {
            currentSequence = "";
            currentTime = 0f;
        }

        if (Input.GetKeyDown("r"))
        {
            currentSequence += 'r';
        } 

        
        if (Input.GetKeyDown("e"))
        {
            currentSequence += 'e';
        } 

        if (Input.GetKeyDown("t"))
        {
            currentSequence += 't';
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
