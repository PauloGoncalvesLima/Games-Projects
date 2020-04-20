using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    public GameObject Player;
    public float changeState;
    public float bobRate;
    private bool flipXState;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        flipXState = false;
        bobRate = 0.001f;
        changeState = 1f;
    }
    // Update is called once per frame-0.28I M-0.3 -0.4F
    void Update()
    {
        
        flipXState = Player.GetComponent<SpriteRenderer>().flipX;
        if(Mathf.Abs(Player.GetComponent<Rigidbody2D>().velocity.x) > 0.1){
            this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y + bobRate * Time.deltaTime, 0);
            if(this.transform.localPosition.y > -0.2f || this.transform.localPosition.y < -0.4f){
                bobRate = bobRate * -1;
            } 

        } else {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, -0.3f, 0); 
        }
        if(!flipXState){
            this.transform.GetComponent<SpriteRenderer>().flipX = flipXState;
            this.transform.GetComponent<SpriteRenderer>().sortingOrder = 65;
            this.transform.localPosition = new Vector3(0, this.transform.localPosition.y, 0); 

        } else {
            this.transform.GetComponent<SpriteRenderer>().flipX = flipXState;
            this.transform.GetComponent<SpriteRenderer>().sortingOrder = 35;
            this.transform.localPosition = new Vector3(-0.36f, this.transform.localPosition.y, 0); 
        }
            
    }
}
