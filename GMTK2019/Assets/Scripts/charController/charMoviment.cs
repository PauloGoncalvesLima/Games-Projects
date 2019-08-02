using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMoviment : MonoBehaviour
{
    public static bool hasLight;
    public float velocity;
    // Start is called before the first frame update
    void Awake()
    {
        velocity = 1.5f;
        hasLight = true; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hasLight == true)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0);
        }
    }
}
