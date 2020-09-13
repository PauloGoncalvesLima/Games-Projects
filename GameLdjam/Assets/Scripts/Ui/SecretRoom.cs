using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretRoom : MonoBehaviour
{

    bool fade;
    public float max, min, changeTime, interpolator;

    // Start is called before the first frame update
    void Start()
    {
        fade = false;
        max = 1.0f;
        changeTime = 1.0f;
        min = 0.0f;
        interpolator = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (fade) {
            interpolator += changeTime * Time.deltaTime;
        } else {
            interpolator -= changeTime * Time.deltaTime;
        }

        if (interpolator > 1.0f){
            interpolator = 1f;
        }
        if (interpolator < 0f) {
            interpolator = 0f;
        }

        this.GetComponent<Tilemap>().color = new Color(this.GetComponent<Tilemap>().color.r, this.GetComponent<Tilemap>().color.g, this.GetComponent<Tilemap>().color.b, Mathf.Lerp(max, min, interpolator));
        
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        fade = true; 
    }

    private void OnTriggerExit2D(Collider2D other) {
        fade = false;
    }
}