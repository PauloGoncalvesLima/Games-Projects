using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FadeOut());
            Brid_Controller.HasBegun = true;
            FindObjectOfType<AudioManager>().Play("Wing");
            GameObject.Find("bird").GetComponent<Gravidade>().enabled = true;
            GameObject.Find("creator_of_pipes").GetComponent<Creator_Pipes>().enabled = true;
        }
    }

    IEnumerator FadeOut()
    {
        Color color = this.gameObject.GetComponent<SpriteRenderer>().color;
        float t = 0;

        while (t < 2f)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / 2f);
            color.a = Mathf.Lerp(color.a, 0, blend);
            this.gameObject.GetComponent<SpriteRenderer>().color = color;

            if (color.a == 0) { this.gameObject.SetActive(false); }

            yield return null;
        }
    }
}
