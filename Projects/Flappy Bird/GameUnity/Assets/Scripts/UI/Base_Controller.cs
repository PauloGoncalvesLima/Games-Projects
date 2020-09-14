using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Controller : MonoBehaviour
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
    private void FixedUpdate()
    {
        if(this.gameObject.transform.position.x <= -3.12)
        {
            this.gameObject.transform.position += new Vector3(this.gameObject.GetComponent<BoxCollider2D>().size.x * 2f, 0, 0);
        }

        if (Brid_Controller.life == false)
        {
            Velocidade = 0;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(Velocidade, 0);
        }     
    }
}
