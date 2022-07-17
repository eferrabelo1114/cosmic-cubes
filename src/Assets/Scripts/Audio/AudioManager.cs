using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static double masterVolume = 0.5;
    public static double musicVolume = 0.5;
    public static double sfxVolume = 1.0;

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
                musicSource.volume = Mathf.Lerp((float)musicVolume, 0, timeElapsed / timeToFade);
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
                musicSource.volume = Mathf.Lerp(0, (float)musicVolume, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            musicSource.volume = (float)musicVolume;
        } else {
            musicSource.clip = newClip;
            musicSource.volume = 0f;
            musicSource.Play();
        
            while(timeElapsed < timeToFade) {
                musicSource.volume = Mathf.Lerp(0, (float)musicVolume, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;

                yield return null;
            }
            musicSource.volume = (float)musicVolume;
        }
    }

    void PlayMusicClip(AudioClip newClip) {
        musicSource.clip = newClip;
        musicSource.Play();
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
        AudioListener.volume = (float)masterVolume;
        effectsSource.volume = (float)sfxVolume;
    }

    public void ChangeMusicVolume() {

        if ((musicVolume - 0.1) <= 0.0) {
            musicVolume = 1.0;
        } else {
            musicVolume -= 0.1;
        }

        musicSource.volume = (float)musicVolume;
    }

    public void ChangeMasterVolume() {
        if ((masterVolume - 0.1) <= 0.0) {
            masterVolume = 1.0;
        } else {
            masterVolume -= 0.1;
        }

        AudioListener.volume = (float)masterVolume;
    }

    public void PlaySound(string clip) {
        AudioClip audioClip = audioAssets.getAudioClip(clip);
        effectsSource.PlayOneShot(audioClip);
    }

    public void PlayMusic(string clip, bool fadeTracks, bool loopTrack) {
        if (currentPlayingTrack == clip) { return; }

        AudioClip audioClip = audioAssets.getAudioClip(clip);
        StopAllCoroutines();
        
        musicSource.loop = loopTrack;

        if (fadeTracks) {
            StartCoroutine(FadeTrack(audioClip));
        } else {
            PlayMusicClip(audioClip);
        }

        currentPlayingTrack = clip;
        isPlayingTrack = true;
    }

    public float GetTrackLength(string clip) {
        AudioClip audioClip = audioAssets.getAudioClip(clip);
        return audioClip.length;
    }
}
