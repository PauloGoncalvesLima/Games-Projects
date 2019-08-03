using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class misController : MonoBehaviour
{
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("ASAD");
        if (collision.gameObject.tag.Equals("Dis"))
        {
            transform.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Dis"))
        {
            transform.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
