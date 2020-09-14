using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crusherController : MonoBehaviour
{
    public bool isCrushed;
    public float speed;
    public float size;

    AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        isCrushed = false;
        speed = 10f;
        size = 8f;

        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCrushed)
        {
            transform.localScale -= Vector3.up * .2f * speed * Time.deltaTime;
            transform.localPosition += Vector3.up * .1f * speed * Time.deltaTime;
            if (transform.localScale.y < 2f)
            {
                isCrushed = false;
            }
        }
        else
        {
            transform.localScale += Vector3.up * .2f * speed * 3 * Time.deltaTime;
            transform.localPosition -= Vector3.up * .1f * speed * 3 * Time.deltaTime;
            if (transform.localScale.y > size)
            {
                isCrushed = true;
                source.Play();
            }
        }
    }
}
