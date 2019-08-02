using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class sceneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1, LoadSceneMode.Single);

    }
}
