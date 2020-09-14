using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sounds[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.Volume;
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, Sounds => Sounds.Name == name);
        s.source.Play();
    }

    public float PlayingTime(string name)
    {
        Sounds s = Array.Find(sounds, Sounds => Sounds.Name == name);
        return s.source.clip.length;
    }


}
