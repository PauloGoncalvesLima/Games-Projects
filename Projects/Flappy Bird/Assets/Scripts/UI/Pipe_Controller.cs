using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_Controller : MonoBehaviour
{
    public float Velocidade;

    // Start is called before the first frame update

    private void Awake()
    {
        Velocidade = -1;
    }
    void Start()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(Velocidade, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.gameObject.transform.position.x <= -2)
        {
            Destroy(this.gameObject);
            
        }

        if (Brid_Controller.life == false)
        {
            Velocidade = 0;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(Velocidade, 0);
        }
    }

    public void DesativarColiders()
    {
        this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
    }
}
