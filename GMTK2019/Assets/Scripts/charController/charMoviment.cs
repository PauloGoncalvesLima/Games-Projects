using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMoviment : MonoBehaviour
{
    public float velocity;
    Rigidbody2D Char;

    // Start is called before the first frame update
    void Awake()
    {
        Char = gameObject.GetComponent<Rigidbody2D>();
        velocity = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Char.velocity = new Vector2(velocity, 0);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            velocity = 0;
        }
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Coner"))
        {
            velocity = 1.5f;
        }

        if (collision.gameObject.tag.Equals("Center"))
        {
            velocity = 0;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Light"))
        {
            velocity = -2.5f;
        }

    }
    
}
