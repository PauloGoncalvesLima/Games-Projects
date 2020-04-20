using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 8.5f;
    public float Grav;
    public GameObject Pause;
    public GameObject Light;
    public GameObject BatterySlider, HopeSlider;

    private Collider2D EventCollider;
    private bool onLadder;
    private bool isPaused;
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
        if(Input.GetKeyDown("e") && !Pause.GetComponent<Pause>().isPaused){
            handleEvent(EventCollider);
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
                EventInfo info = other.GetComponent<EventInfo>();
                if (info.type == EventInfo.Types.PassDialogue) {
                    StartCoroutine(dm.startDialogue(info.dialogue));
                    Destroy(other.gameObject);
                    
                } else {
                    dm.showInteract();
                    EventCollider = other;
                }
                
                break;
        }
    }

    private void handleEvent(Collider2D other) {
        EventInfo info = other.GetComponent<EventInfo>();
        switch(info.type) {
            case EventInfo.Types.Dialogue:
                StartCoroutine(dm.startDialogue(info.dialogue));
                Destroy(other.gameObject);
                break;
            case EventInfo.Types.Battery:
                if (!BatterySlider.activeSelf) {
                    BatterySlider.SetActive(true);
                    HopeSlider.SetActive(true);
                }
                StartCoroutine(dm.startDialogue(info.dialogue));
                BatterySlider.GetComponent<BatteryUi>().addBattery();
                Destroy(other.gameObject);
                break;
            case EventInfo.Types.Hope:
                StartCoroutine(dm.startDialogue(info.dialogue));
                HopeSlider.GetComponent<HopeUI>().addHope(info.hopeAmmount);
                Destroy(other.gameObject);
                break;
            case EventInfo.Types.Mine:
                if(this.transform.GetChild(0).gameObject.activeSelf){
                    string[] phrases = new string[] {"Uff", "Uhg", "Sigh", "Huff", "Pant"};
                    StartCoroutine(dm.startDialogue(phrases[Random.Range(0, 5)]));
                    Destroy(other.gameObject);
                } else {
                    StartCoroutine(dm.startDialogue("Sigh... I don't have a pickaxe to break this."));
                }
                break;
            case EventInfo.Types.Pickaxe:
                this.transform.Find("Pickaxe").gameObject.SetActive(true);
                StartCoroutine(dm.startDialogue(info.dialogue));
                HopeSlider.GetComponent<HopeUI>().addHope(info.hopeAmmount);
                Destroy(other.gameObject);
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
                EventCollider = null;
                break;
        }
    }
}