using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

    public string name;
    public AudioClip[] clips;
    [HideInInspector]
    public int clipIndex = 0;

    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public void addIndex() {
        clipIndex = (clipIndex + 1) % clips.Length;
    }
}