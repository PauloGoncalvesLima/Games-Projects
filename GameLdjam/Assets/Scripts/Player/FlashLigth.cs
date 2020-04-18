using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLigth : MonoBehaviour
{

    [SerializeField]
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {   
        if(Player.GetComponent<Rigidbody2D>().velocity.x < -0.5f){
            this.transform.eulerAngles = Vector3.forward * -90f;
        } else if (Player.GetComponent<Rigidbody2D>().velocity.x > 0.5f) {
            this.transform.eulerAngles = Vector3.forward * 90f;
        }
        Vector3 pos = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        this.transform.position = pos;
    }
}
