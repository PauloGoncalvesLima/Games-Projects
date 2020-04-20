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
    public bool ConversationIsOver;

    private Rigidbody2D rb;
    private DialogueManager dm;
    private DialogueManager MinerDialog;
    private Animator anim;
    private void Awake() {
        // fetch components
        rb = GetComponent<Rigidbody2D>();
        dm = GetComponentInChildren<DialogueManager>();
        MinerDialog = transform.GetChild(2).GetComponent<DialogueManager>();
        anim = GetComponent<Animator>();
        // initial properties
        onLadder = false;
        ConversationIsOver = true;
    }

    private void FixedUpdate() {
        if(!Pause.GetComponent<Pause>().isPaused && ConversationIsOver){
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
                    
                } else if(info.type == EventInfo.Types.Conversation){
                    string[] phrasesPlayer = info.ConversationPlayer;
                    string[] phrasesResponse = info.ConversationResponse;
                    HopeSlider.GetComponent<HopeUI>().addHope(info.hopeAmmount);
                    StartCoroutine(Conversation(phrasesPlayer, phrasesResponse, info.firstDialogIsFromPlayer, info.lastDialogIsFromPlayer));
                    Destroy(other.gameObject.GetComponent<BoxCollider2D>());
                }else {
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

    public IEnumerator Conversation(string[] Player, string[] Response, bool firstDialogIsFromPlayer, bool lastDialogIsFromPlayer){
        int ConversationStage = 0;
        ConversationIsOver = false;
        rb.velocity = new Vector2(0, 0);
        anim.SetFloat("Speed",0);
        while(!ConversationIsOver){
            // Debug.Log(Player.Length);
            if(!firstDialogIsFromPlayer){ //Dialog starts wiht the NPC

                StartCoroutine(dm.responseDialog(Response[ConversationStage]));
                yield return new WaitForSeconds(2.2f);
                StartCoroutine(dm.startDialogue(Player[ConversationStage]));
                yield return new WaitForSeconds(2.2f);

            } else if(firstDialogIsFromPlayer){//Dialog starts wiht the Player
                StartCoroutine(dm.startDialogue(Player[ConversationStage]));
                yield return new WaitForSeconds(2.2f);
                StartCoroutine(dm.responseDialog(Response[ConversationStage]));
                yield return new WaitForSeconds(2.2f);
            }

            if(lastDialogIsFromPlayer && Player.Length -1 == ConversationStage){
                ConversationIsOver = true;
            } else if(!lastDialogIsFromPlayer && Response.Length - 1 == ConversationStage){
                ConversationIsOver = true;
            }
            ConversationStage++;
        }
        ConversationIsOver = true;
        yield return 0;
    }
}