using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLigth : MonoBehaviour
{
    public GameObject Player;
    Animator playerAnim;
    SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = Player.GetComponent<Animator>();
        playerSprite = Player.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {   
        if (playerAnim.GetBool("LookUp")) {
            this.transform.eulerAngles = Vector3.forward;
        } else if (playerSprite.flipX) {
            this.transform.eulerAngles = Vector3.forward *  90f;
        } else {
            this.transform.eulerAngles = Vector3.forward * - 90f;
        }
        Vector3 pos = new Vector3(Player.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.transform.position = pos;
    }
}
