using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent : MonoBehaviour
{
    charAudio sounds;

    private void Start()
    {
        sounds = gameObject.GetComponentInParent<charAudio>();
    }

    public void GaspEvent()
    {
        sounds.Gasp();
    }

    public void StepEvent()
    {
        sounds.Step();
    }

    public void LStepEvent()
    {
        sounds.LStep();
    }

    public void ClickEvent()
    {
        sounds.Click();
    }

    public void SquishEvent()
    {
        sounds.Squish();
    }

    public void CryEvent()
    {
        sounds.Cry();
    }
}
