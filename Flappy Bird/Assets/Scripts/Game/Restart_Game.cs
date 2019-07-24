using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_Game : MonoBehaviour
{
   public void RestartGame()
    {
        Debug.Log("ME MATA");
        SceneManager.LoadScene("Game");
    }
}
