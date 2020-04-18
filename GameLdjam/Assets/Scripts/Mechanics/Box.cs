using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private bool playerisTouchingBox;
    public GameObject Player;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerisTouchingBox = false;
    }
  

    // Update is called once per frame
    void Update()
    {
        if(playerisTouchingBox && Input.GetKeyDown(KeyCode.Space)){
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            playerisTouchingBox = true;
            Debug.Log("Enter");
        }
    }
    private void OnTriggerExit2D(Collider2D other) {

        if(other.gameObject.tag == "Player"){
            playerisTouchingBox = false;
            Debug.Log("Exit");
        }
    }
}
