using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 8.5f;
    private bool onLadder;

    private Rigidbody2D rb;

    private void Awake() {
        // fetch components
        rb = GetComponent<Rigidbody2D>();
        
        // initial properties
        onLadder = false;
    }

    private void FixedUpdate() {
        float hVelocity = Input.GetAxisRaw("Horizontal") * speed;
        float vVelocity = rb.velocity.y;

        if (onLadder) {
            vVelocity = Input.GetAxisRaw("Vertical") * speed;
        }

        rb.velocity = new Vector2(hVelocity, vVelocity);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ladder") {
            onLadder = true;
            rb.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Ladder") {
            onLadder = false;
            rb.gravityScale = 2;
        }
    }
}