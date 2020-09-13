using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isPaused, isInACutcene;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        isInACutcene =false;
    }

    public void setPause(bool state){
        isInACutcene = state;
        isPaused = state;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("escape") || Input.GetKeyUp("p")){
            if(!isInACutcene){
                isPaused = !isPaused;
            }
            this.transform.GetChild(0).gameObject.SetActive(isPaused);
        }
    }
}
