using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 8.5f;
    public float Grav;
    public GameObject Pause;
    private bool onLadder;
    private bool canInteract;
    private bool ePress;
    private Rigidbody2D rb;
    private DialogueManager dm;
    private Animator anim;
    
    private void Awake() {
        // fetch components
        rb = GetComponent<Rigidbody2D>();
        dm = GetComponentInChildren<DialogueManager>();
        anim = GetComponent<Animator>();
        // initial properties
        onLadder = false;
        canInteract = false;
        ePress = false;
    }

    private void FixedUpdate() {
        if(!Pause.GetComponent<Pause>().isPaused){
            handleMovement();
        }
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown("e")){
            ePress = true;
        } else if (Input.GetKeyUp("e")){
            ePress = false;
        }
    }

    private void handleMovement() {
        float hVelocity = Input.GetAxisRaw("Horizontal") * speed;
        float vVelocity = rb.velocity.y;

        if (onLadder) {
            vVelocity = Input.GetAxisRaw("Vertical") * speed;
        }

        // animation setup
        anim.SetFloat("Speed",Mathf.Abs(hVelocity));

        // look up
        if (Input.GetAxisRaw("Vertical") > 0.1 && Mathf.Abs(hVelocity) < 0.1) {
            anim.SetBool("LookUp",true);
        } else {
            anim.SetBool("LookUp",false);
        }

        rb.velocity = new Vector2(hVelocity, vVelocity);
        if(rb.velocity.x < -0.5 ){
            this.GetComponent<SpriteRenderer>().flipX = true;
        } else if (rb.velocity.x > 0.5) {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "Ladder":
                onLadder = true;
                rb.gravityScale = 0;
                break;
            case "Event":
                dm.showInteract();
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "Event":
                if (ePress) {
                    StartCoroutine(dm.startDialogue(other.gameObject.name));
                    Destroy(other.gameObject);
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "Ladder":
                onLadder = false;
                rb.gravityScale = Grav;
                break;
            case "Event":
                dm.hideInteract();
                break;
        }
    }
}