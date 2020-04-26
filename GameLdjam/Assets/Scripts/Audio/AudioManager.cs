using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;

    // public static AudioManager instance;

    private void Awake() {

        // if (instance == null)
        //     instance = this;
        // else {
        //     Destroy(gameObject);
        //     return;
        // }

        // DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clips[0];

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Use to play soundtrack
    private void Start() {
        this.Play("Soundtrack");
    }

    // outside usage: FindObjectOfType<AudioManager>().Play("name")
    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        if (s.clips.Length > 1) {
            s.addIndex();
            s.source.clip = s.clips[s.clipIndex];
        }
        s.source.Play();
    }

    public void Stop(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        s.source.Stop();
    }

}