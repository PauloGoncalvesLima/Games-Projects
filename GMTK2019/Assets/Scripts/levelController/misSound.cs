using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class misSound : MonoBehaviour
{
    public AudioSource mis;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Char"))
        {
            mis.Play();
        }
    }
}
