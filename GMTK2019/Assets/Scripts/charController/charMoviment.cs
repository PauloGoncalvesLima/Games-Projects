using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMoviment : MonoBehaviour
{
    public static bool hasLight;
    public static bool hitWall;
    public float velocity;
    // Start is called before the first frame update
    void Awake()
    {
        velocity = 1.5f;
        hasLight = false;
        hitWall = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hasLight == true)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0);
        }
        else if(!hitWall)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity-1, 0);
        }

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall") && velocity < 0)
        {
            Debug.Log("Wall");
            hitWall = true;
            velocity = 0;
        }
    }
}
