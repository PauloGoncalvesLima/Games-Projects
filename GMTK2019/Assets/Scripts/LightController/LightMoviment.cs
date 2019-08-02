using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMoviment : MonoBehaviour
{
    float Ca, Co;
    bool mouseDown;
    Vector3 mPos;

    private void Awake()
    {
        mouseDown = false;
    }

    private void FixedUpdate()
    {
        if (mouseDown)
        {
            mPos = Input.mousePosition;
            mPos = Camera.main.ScreenToWorldPoint(mPos);

            Co = -mPos.x + this.transform.position.x;
            Ca = -mPos.y + this.transform.position.y;

            Vector2 Direction = new Vector2(Co, Ca);
            transform.up = Direction;
            //Debug.Log(transform.up);
        }
        
    }
   
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }
    }

    private void OnMouseUp()
    {
        mouseDown = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Char"))
        {
            charMoviment.hasLight = true;
        }
   
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Char"))
        {
            charMoviment.hasLight = false;
        }
    }

}
