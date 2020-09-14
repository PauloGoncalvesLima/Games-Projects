using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charAudio : MonoBehaviour
{
    public AudioClip[] steps;
    public AudioClip[] lSteps;
    public AudioSource step;
    public AudioSource gasp;
    public AudioSource click;
    public AudioSource squish;
    public AudioSource cry;
    public AudioSource land;

    int stepCounter;

    private void Start()
    {
        stepCounter = 0;
    }

    public void Step()
    {
        step.clip = steps[stepCounter];
        step.Play();
        stepCounter = (stepCounter + 1) % 3;
    }

    public void LStep()
    {
        step.clip = lSteps[stepCounter];
        step.Play();
        stepCounter = (stepCounter + 1) % 3;
    }

    public void Gasp()
    {
        gasp.Play();
    }

    public void Click()
    {
        click.Play();
    }

    public void Squish()
    {
        squish.Play();
    }

    public void Cry()
    {
        cry.Play();
    }

    public void Land()
    {
        land.Play();
    }
}
