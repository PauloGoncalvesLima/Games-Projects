using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameOver : MonoBehaviour
{
    public void onClick(){
        SceneManager.LoadScene(0,  LoadSceneMode.Single);
    }
}
