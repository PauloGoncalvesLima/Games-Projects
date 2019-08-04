using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMoviment : MonoBehaviour
{

    public float escadaV;

    public float velocity;
    public float velocityY;

    public bool isScared;

    Rigidbody2D Char;
    Animator animDark;
    Animator animLight;

    bool isEnt;
    bool isRaising;
    bool isExiting;

    float t, charCenter, stairenter, extentStair;


    public int direction;

    // Start is called before the first frame update
    void Awake()
    {
        isScared = false;
        escadaV = -1.5f;
        velocityY = 15;

        animDark = transform.GetChild(0).GetComponent<Animator>();
        animLight = transform.GetChild(1).GetComponent<Animator>();
        Char = gameObject.GetComponent<Rigidbody2D>();
        velocity = 0f;

        Char.gravityScale = velocityY;


        isEnt = false;
        isRaising = false;
        isExiting = false;

        t = 0;
        stairenter = 0;
        charCenter = 0;
        direction = 1;
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

        if (isEnt)
        {
            velocity = 0;
            t += Time.deltaTime / 2;
            transform.localPosition = new Vector3(Mathf.Lerp(charCenter, stairenter, t), gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            if(transform.localPosition.x == stairenter)
            {
                Char.gravityScale = escadaV;
                charCenter = transform.localPosition.x;
                isEnt = false;
                isRaising = true;
                t = 0;
            }
        }
        if (isExiting)
        {
            velocity = 0;
            t += Time.deltaTime / 2;
            transform.localPosition = new Vector3(Mathf.Lerp(charCenter, charCenter+ (extentStair*3) * direction, t), gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            if (transform.localPosition.x == extentStair * direction)
            {
                isEnt = false;
                isRaising = false;
                isExiting = false;
                Char.gravityScale = 15;
                velocity = 1.5f;
                t = 0;
            }
        }


    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            velocity = 0;
            
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Death"))
        {
            // die

        }
        if (collision.gameObject.tag.Equals("Escada") && !isEnt)
        {
            isEnt = true;
            velocity = 0;
            Char.gravityScale = 0;
            Char.velocity = new Vector2(0f, 0);
            charCenter = gameObject.transform.localPosition.x;
            stairenter = collision.gameObject.transform.GetComponent<BoxCollider2D>().bounds.center.x;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Light") && (!isEnt && !isRaising && !isExiting))
        {
            animDark.SetBool("isScared", false);
            animLight.SetBool("isScared", false);
            isScared = false;
        }

        if (collision.gameObject.tag.Equals("Coner") && (!isEnt && !isRaising && !isExiting))
        {
            velocity = 1.5f;
        }

        if (collision.gameObject.tag.Equals("ConerD") && (!isEnt && !isRaising && !isExiting))
        {
            velocity = -1.5f;
        }

        if (collision.gameObject.tag.Equals("Center"))
        {
            velocity = 0;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Light") && (!isEnt && !isRaising && !isExiting))
        {
            animDark.SetBool("isScared", true);
            animLight.SetBool("isScared", true);
            isScared = true;
        }

        if (collision.gameObject.tag.Equals("Escada"))
        {
            Char.gravityScale = 0;
            velocity = 0;
            extentStair = collision.gameObject.transform.GetComponent<BoxCollider2D>().bounds.extents.x;
            isRaising = false;
            isExiting = true;
        }

    }
    
}
