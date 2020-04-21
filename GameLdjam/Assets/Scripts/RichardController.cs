using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichardController : MonoBehaviour
{
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public void faint() {
        anim.SetBool("Faint",true);
    }
}
