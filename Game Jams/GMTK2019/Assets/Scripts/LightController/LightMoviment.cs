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
            if(transform.GetComponent<Rigidbody2D>().rotation <= -65 || transform.GetComponent<Rigidbody2D>().rotation >= 65)
            {
                mouseDown = false;
                transform.GetComponent<Rigidbody2D>().gravityScale = 1;
                if (transform.GetComponent<Rigidbody2D>().angularVelocity >= 100)
                {
                    transform.GetComponent<Rigidbody2D>().angularVelocity = 100f;
                }
                else if (transform.GetComponent<Rigidbody2D>().angularVelocity <= -100)
                {
                    transform.GetComponent<Rigidbody2D>().angularVelocity = -100f;
                }
            }
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
            transform.GetComponent<Rigidbody2D>().gravityScale = 0;
            
        }
    }

    private void OnMouseUp()
    {
        mouseDown = false;
        transform.GetComponent<Rigidbody2D>().gravityScale = 1;
        if (transform.GetComponent<Rigidbody2D>().angularVelocity >= 100)
        {
            transform.GetComponent<Rigidbody2D>().angularVelocity = 100f;
        }else if(transform.GetComponent<Rigidbody2D>().angularVelocity <= -100)
        {
            transform.GetComponent<Rigidbody2D>().angularVelocity = -100f;
        }
        
    }

}
