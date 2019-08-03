using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMoviment : MonoBehaviour
{
    public float velocity;
    Rigidbody2D Char;
    Animator animDark;
    Animator animLight;

    // Start is called before the first frame update
    void Awake()
    {
        animDark = transform.GetChild(0).GetComponent<Animator>();
        animLight = transform.GetChild(1).GetComponent<Animator>();
        Char = gameObject.GetComponent<Rigidbody2D>();
        velocity = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Char.velocity = new Vector2(velocity, 0);
        if (velocity == 0)
        {
            animDark.SetBool("isStill", true);
            animLight.SetBool("isStill", true);
        }
        else
        {
            animDark.SetBool("isStill", false);
            animLight.SetBool("isStill", false);
        }
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

        if (collision.gameObject.tag.Equals("Light"))
        {
            animDark.SetBool("isScared", false);
            animLight.SetBool("isScared", false);
        }

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
            animDark.SetBool("isScared", true);
            animLight.SetBool("isScared", true);
        }

    }
    
}
