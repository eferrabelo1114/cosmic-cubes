using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private float musicVolume = 0.3f;
    private float sfxVolume = 1.0f;

    private bool isPlayingTrack = false;
    private string currentPlayingTrack = null;

    [SerializeField] private AudioSource musicSource, effectsSource;

    public static AudioManager instance;
    public AudioAssets audioAssets;

    private IEnumerator FadeTrack(AudioClip newClip) {
        float timeToFade = 0.25f;
        float timeElapsed = 0;

        if (isPlayingTrack) {
            // Fade track out
            while(timeElapsed < timeToFade) {
                musicSource.volume = Mathf.Lerp(musicVolume, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            musicSource.Stop();

            // Fade next track in
            musicSource.clip = newClip;
            musicSource.volume = 0;

            timeElapsed = 0;
            musicSource.Play();
            while(timeElapsed < timeToFade) {
                musicSource.volume = Mathf.Lerp(0, musicVolume, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            musicSource.volume = musicVolume;
        } else {
            musicSource.clip = newClip;
            musicSource.volume = 0f;
            musicSource.Play();
        
            while(timeElapsed < timeToFade) {
                musicSource.volume = Mathf.Lerp(0, musicVolume, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;

                yield return null;
            }
            musicSource.volume = musicVolume;
        }
    }


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        effectsSource.volume = sfxVolume;
    }

    public void PlaySound(string clip) {
        AudioClip audioClip = audioAssets.getAudioClip(clip);
        effectsSource.PlayOneShot(audioClip);
    }

    public void PlayMusic(string clip) {
        if (currentPlayingTrack == clip) { return; }

        AudioClip audioClip = audioAssets.getAudioClip(clip);
        StopAllCoroutines();
        Debug.Log("Play music");
        StartCoroutine(FadeTrack(audioClip));

        currentPlayingTrack = clip;
        isPlayingTrack = true;
    }
}
