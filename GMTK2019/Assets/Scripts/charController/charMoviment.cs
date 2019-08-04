using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMoviment : MonoBehaviour
{

    public float escadaV;

    public float velocity;
    public float velocityY;

    int stage;

    public bool isScared;
    public static bool life;

    Rigidbody2D Char;
    SpriteRenderer spriteDark;
    SpriteRenderer spriteLight;
    Animator animDark;
    Animator animLight;
    BoxCollider2D stair;

    bool isEnt;
    bool isRaising;
    bool isExiting;
    
    float t, charCenter, xPos;

    public int direction;

    // Start is called before the first frame update
    void Awake()
    {
        isScared = false;
        life = true;
        escadaV = -1.5f;

        velocity = 0f;
        velocityY = 15;

        animDark = transform.GetChild(0).GetComponent<Animator>();
        animLight = transform.GetChild(1).GetComponent<Animator>();
        spriteDark = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteLight = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Char = gameObject.GetComponent<Rigidbody2D>();

        Char.gravityScale = velocityY;

        stage = 0;

        isEnt = false;
        isRaising = false;
        isExiting = false;

        t = 0;
        charCenter = 0f;
        direction = 1;
        xPos = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Char.velocity = new Vector2(velocity, Char.velocity.y);
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

        if (velocity > 0)
        {
            spriteDark.flipX = false;
            spriteLight.flipX = false;
        }
        else if (velocity < 0)
        {
            spriteDark.flipX = true;
            spriteLight.flipX = true;
        }

        // check if falling
        if (Char.velocity.y < 0f)
        {
            if (animDark.GetBool("isFalling") == false)
            {
                animDark.SetTrigger("StartFall");
                animLight.SetTrigger("StartFall");
                animDark.SetBool("isFalling", true);
                animLight.SetBool("isFalling", true);
            }
        }
        else
        {
            if (animDark.GetBool("isFalling") == true)
            {
                animDark.SetBool("isFalling", false);
                animLight.SetBool("isFalling", false);
            }
        }

        if (isEnt)
        {

            if (stage == 0)
            {
                animDark.SetBool("isStill", false);
                //animLight.SetBool("isStill", false);
                velocity = 0;
                t += Time.deltaTime / 2;

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(stair.bounds.center.x, transform.position.y, transform.position.z), t);

                if (transform.position.x == stair.bounds.center.x)
                {
                    t = 0;
                    stage++;
                }
            }
            else if (stage == 1)
            {
                animDark.SetBool("isClimbing", true);
                //animLight.SetBool("isClimbing", true);
                velocity = 0;
                Char.gravityScale = 0f;
                t += Time.deltaTime/75;

                Vector3 targetPos = new Vector3(transform.position.x, stair.bounds.center.y + stair.bounds.extents.y + transform.GetComponent<CapsuleCollider2D>().bounds.extents.y + .1f, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, t);
                if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(targetPos.x,targetPos.y,0)) < 0.01f)
                {
                    t = 0;
                    stage++;
                    charCenter = transform.position.x;
                    xPos = transform.position.x;
                    animDark.SetBool("isClimbing", false);
                    //animLight.SetBool("isClimbing", false);
                }
            }
            else if (stage == 2)
            {
                velocity = 0;
                t += Time.deltaTime / 2;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(xPos + (.05f + stair.bounds.size.x) * direction, transform.position.y, transform.position.z), t);
                if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(xPos + (.05f + stair.bounds.size.x) * direction, transform.position.y, 0)) < 0.01f)
                {
                    t = 0;
                    stage = 0;
                    isEnt = false;
                    Char.gravityScale = velocityY;
                    animDark.SetBool("isStill", true);
                    //animLight.SetBool("isStill", true);
                }
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
            if (collision.gameObject.GetComponent<crusherController>().isCrushed == false)
            {
                // die
                velocity = 0;
                Char.gravityScale = 0;
                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                animDark.SetBool("isSquish", true);
                //animLight.SetBool("isSquish", true);
                life = false;
                this.enabled = false;
            }

        }
        if (collision.gameObject.tag.Equals("Escada") && !isEnt)
        {
            isEnt = true;
            velocity = 0;
            stair = collision.GetComponent<BoxCollider2D>();
            direction = collision.gameObject.GetComponent<stairDirection>().direction;
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

    }
    
}
