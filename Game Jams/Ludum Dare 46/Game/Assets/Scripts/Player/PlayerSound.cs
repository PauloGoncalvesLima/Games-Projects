using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    public AudioClip[] footStep;
    public AudioClip ladderSound;
    public int footIndex;
    public AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        footIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Footstep() {
        audioS.PlayOneShot(footStep[footIndex], 0.2f);
        footIndex = (footIndex + 1) % footStep.Length;
    }

    public void LadderClimb() {
        audioS.PlayOneShot(ladderSound, 0.2f);
    }
}
