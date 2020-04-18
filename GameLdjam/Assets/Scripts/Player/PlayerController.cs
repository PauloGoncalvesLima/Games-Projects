using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 8.5f;
    private bool onLadder;
    private bool canInteract;

    private Rigidbody2D rb;
    private DialogueManager dm;

    private void Awake() {
        // fetch components
        rb = GetComponent<Rigidbody2D>();
        dm = GetComponentInChildren<DialogueManager>();
        // initial properties
        onLadder = false;
        canInteract = false;
    }

    private void FixedUpdate() {
        handleMovement();
    }

    private void handleMovement() {
        float hVelocity = Input.GetAxisRaw("Horizontal") * speed;
        float vVelocity = rb.velocity.y;

        if (onLadder) {
            vVelocity = Input.GetAxisRaw("Vertical") * speed;
        }

        rb.velocity = new Vector2(hVelocity, vVelocity);
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
                if (Input.GetKeyDown("e")) {
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
                rb.gravityScale = 2;
                break;
            case "Event":
                dm.hideInteract();
                break;
        }
    }
}