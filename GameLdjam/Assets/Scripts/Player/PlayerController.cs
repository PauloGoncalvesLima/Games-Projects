using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 8.5f;
    public float Grav;
    public GameObject Pause;
    public GameObject Light;
    public GameObject BatterySlider, HopeSlider;
    private bool onLadder;
    private bool ePress;
    private bool canEPress;
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
        ePress = false;
        canEPress = false;
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
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "Event":
                EventInfo info = other.GetComponent<EventInfo>();
                if (info.type == EventInfo.Types.PassDialogue) {
                    StartCoroutine(dm.startDialogue(info.dialogue));
                    Destroy(other.gameObject);
                    break;
                }
                if (!canEPress) {
                    dm.showInteract();
                    canEPress = true;
                }
                if (ePress) {
                    switch(info.type) {
                        case EventInfo.Types.Dialogue:
                            StartCoroutine(dm.startDialogue(info.dialogue));
                            break;
                        case EventInfo.Types.Battery:
                            if (!BatterySlider.activeSelf) {
                                BatterySlider.SetActive(true);
                                HopeSlider.SetActive(true);
                            }
                            StartCoroutine(dm.startDialogue(info.dialogue));
                            BatterySlider.GetComponent<BatteryUi>().addBattery();
                            break;
                        case EventInfo.Types.Hope:
                            StartCoroutine(dm.startDialogue(info.dialogue));
                            HopeSlider.GetComponent<HopeUI>().addHope(info.hopeAmmount);
                            break;
                        case EventInfo.Types.Mine:
                            break;
                    }
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
                canEPress = false;
                break;
        }
    }
}