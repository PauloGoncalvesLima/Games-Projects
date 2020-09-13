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
        bobRate = 0.0145f;
        changeState = 1f;
    }
    private void FixedUpdate() {
        
        if(Mathf.Abs(Player.GetComponent<Rigidbody2D>().velocity.x) > 0.1){
            this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y + bobRate, 0);
            if(this.transform.localPosition.y > 0.24f || this.transform.localPosition.y < 0.09f){
                bobRate = bobRate * -1;
            } 

        } else {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, 0.24f, 0); 
        }
    }

    // Update is called once per frame0.24I M-0.3 F
    void Update()
    {
        flipXState = Player.GetComponent<SpriteRenderer>().flipX;
        
        if(flipXState){
            this.transform.GetComponent<SpriteRenderer>().flipX = flipXState;
            this.transform.localPosition = new Vector3(0.5f, this.transform.localPosition.y, 0); 
            this.transform.localRotation =new Quaternion (this.transform.localRotation.x, 24, this.transform.localRotation.z, this.transform.localRotation.w);
        } else {
            this.transform.GetComponent<SpriteRenderer>().flipX = flipXState;
            this.transform.localPosition = new Vector3(-0.5f, this.transform.localPosition.y, 0);
            this.transform.localRotation =new Quaternion (this.transform.localRotation.x, 66, this.transform.localRotation.z, this.transform.localRotation.w);
        }
            
    }
}
