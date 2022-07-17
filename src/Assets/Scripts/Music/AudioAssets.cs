using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAssets : MonoBehaviour
{
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private Object[] audioClipObjs;

    void Awake()
    {
        audioClipObjs = Resources.LoadAll("Audio", typeof(AudioClip));

        foreach (var clip in audioClipObjs) {
            audioClips.Add(clip.name, (AudioClip)clip);
        }
    }

    public AudioClip getAudioClip(string name) {
        AudioClip temp = null;
        if (!audioClips.TryGetValue(name, out temp)) { return null; }

        return temp;
    }
}
