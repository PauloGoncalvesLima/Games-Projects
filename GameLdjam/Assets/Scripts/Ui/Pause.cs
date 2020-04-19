using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    public bool getisPaused(){
        return isPaused;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("escape") || Input.GetKeyUp("p")){
            isPaused = !isPaused;
            this.transform.GetChild(0).gameObject.SetActive(isPaused);
        }
    }
}
