using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fearBar : MonoBehaviour
{
    GameObject Char;
    public float speed;
    public float speed2;
    public float size;
    public float terror;
    public float controler;
    public Color tmp;
    public Color startColor;
    public Color endColor;
    // Start is called before the first frame update
    void Start()
    {
        speed = 15;
        speed2 = 7;
        size = 250;
        terror = size / 2;
        controler = -3.5f;
         
        Char = GameObject.Find("Character");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Char.GetComponent<charMoviment>().isScared)
        {
            transform.localScale += Vector3.right * Time.deltaTime * speed;
        }
        else if (!Char.GetComponent<charMoviment>().isScared)
        {
            transform.localScale -= Vector3.right * Time.deltaTime * speed2;
        }


        controler = Mathf.Lerp(-3.5f, -0.1f,transform.localScale.x / size);


        if (transform.localScale.x >= size)
        {
            transform.localScale = new Vector3 (size, transform.localScale.y, transform.localScale.z);
        }
        else if (transform.localScale.x <= 0)
        {
            transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
        }

        if(transform.localScale.x>=terror && transform.localScale.x <= size)
        {
            transform.localPosition += Vector3.right * Time.deltaTime * Random.Range(-terror, terror) / terror * transform.localScale.x / controler;
            transform.localPosition += Vector3.up * Time.deltaTime * Random.Range(-terror, terror) / terror * transform.localScale.x / controler;

            if (transform.localPosition.x >= -949.7)//Max
            {
                transform.localPosition = new Vector3(-949.7f, transform.localPosition.y, transform.localPosition.z);
            }
            else if (transform.localPosition.x <= -964.1) // Min
            {
                transform.localPosition = new Vector3(-964.1f, transform.localPosition.y, transform.localPosition.z);
            }

            if (transform.localPosition.y >= 381.8) //Max
            {
                transform.localPosition = new Vector3(transform.localPosition.x, 381.8f, transform.localPosition.z);
            }
            else if (transform.localPosition.y <= 371.2)//MIn
            {
                transform.localPosition = new Vector3(transform.localPosition.x, 371.2f, transform.localPosition.z);
            }

        }
        else
        {
            transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, -948.9f, Time.deltaTime * 10), Mathf.Lerp(transform.localPosition.y, 376.2f, Time.deltaTime * 10), transform.localPosition.z);
        }


        if (transform.localScale.x > 0 && transform.localScale.x <= size)
        {
            tmp = Color.Lerp(startColor, endColor, transform.localScale.x/ size);
            transform.GetComponentInChildren<SpriteRenderer>().color = new Color(tmp.r,tmp.g,tmp.b,100);
            
        }

       
    }

}
