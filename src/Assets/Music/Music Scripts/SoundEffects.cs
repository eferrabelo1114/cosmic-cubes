using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource AudioSrce;
    public int song = -1;
    [SerializeField]public List<AudioClip> soundEffects = new List<AudioClip>();
    void Awake()
    {
        AudioSrce = this.GetComponent<AudioSource>();
        

        AudioClip move = Resources.Load<AudioClip>("Audio/sfx/move");
        soundEffects.Add(move);

    }
    public void playOnce(AudioClip sound){
        AudioSrce.PlayOneShot(sound);
    }
}
