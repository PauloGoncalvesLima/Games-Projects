﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sounds
{
    public string Name;
   
    public AudioClip clip;
   
    [Range(0f, 100f)]
    public float Volume;

    [HideInInspector]
    public AudioSource source;
}
