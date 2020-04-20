using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject[] SetActiveFirst;
    public GameObject[] ObjectsToActivate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void onClick(){
        for(int i = 0; i< ObjectsToActivate.Length; i++){
            SetActiveFirst[i].SetActive(true);
        }
        for(int i = 0; i< ObjectsToActivate.Length; i++){
            ObjectsToActivate[i].SetActive(true);
        }
        this.gameObject.SetActive(false);
    }
}
