using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravidade : MonoBehaviour
{

    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody2D>();
        body.gravityScale = 1;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (body.velocity.y < -2) { body.velocity = new Vector2(body.velocity.x, -2); }

        // rotate down
        if (this.gameObject.transform.eulerAngles.z > 25 && this.gameObject.transform.eulerAngles.z <= 31) { this.gameObject.transform.Rotate(Vector3.forward * -0.75f); }

        if (this.gameObject.transform.eulerAngles.z > 359 || this.gameObject.transform.eulerAngles.z <= 25) { this.gameObject.transform.Rotate(Vector3.forward * -1.4f); }


        if (this.gameObject.transform.eulerAngles.z <= 359 && this.gameObject.transform.eulerAngles.z >= 270) {
            this.gameObject.transform.Rotate(Vector3.forward * -5f);
            body.velocity = new Vector2(body.velocity.x, -5f);

        }
    }
}
