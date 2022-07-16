using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource AudioSrce;
    public int song = -1;
    [SerializeField]public List<AudioClip> soundEffects = new List<AudioClip>();
    [SerializeField]public List<AudioClip> backgroundMusic = new List<AudioClip>();
    // Start is called before the first frame update
    void Awake()
    {
        AudioSrce = this.GetComponent<AudioSource>();
        AudioSrce.loop = true;
        AudioClip stage0 = Resources.Load<AudioClip>("Audio/BackgroundMusic/1");
        backgroundMusic.Add(stage0);
        AudioClip stage1 = Resources.Load<AudioClip>("Audio/BackgroundMusic/2");
        backgroundMusic.Add(stage1);
        AudioClip stage2 = Resources.Load<AudioClip>("Audio/BackgroundMusic/3");
        backgroundMusic.Add(stage2);
        
    }

    public void playMusic(AudioClip music, int songnum){
        if(song!=songnum){
        song = songnum;
        AudioSrce.clip = music;
        AudioSrce.Play();
        }
    }
}
