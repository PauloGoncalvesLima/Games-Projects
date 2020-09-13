using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brid_Controller : MonoBehaviour
{

    Rigidbody2D body;
    Animator BirdFlapController;
    public static int points;
    public static bool life;
    public static bool HasBegun;
    float t;
    bool State;
    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        State = false;
        body = this.gameObject.GetComponent<Rigidbody2D>();
        BirdFlapController = this.gameObject.GetComponent<Animator>();
        life = true;
        HasBegun = false;
        points = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (!life)
        {
            this.enabled = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            body.velocity = new Vector2(0, 3);
            BirdFlapController.SetFloat("FlapSpeed", 1f);
            this.gameObject.transform.eulerAngles = Vector3.forward * 30;
            FindObjectOfType<AudioManager>().Play("Wing");
        }
        if(BirdFlapController.GetFloat("FlapSpeed") > 0.5)
        {

            BirdFlapController.SetFloat("FlapSpeed", BirdFlapController.GetFloat("FlapSpeed")-0.1f);
        }

        if(this.gameObject.transform.position.y>= 2.80f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 2.80f, this.gameObject.transform.position.z);
        }
        
    }

    private void FixedUpdate()
    {
        if (!HasBegun)
        {
            if (!State)
            {
                t += Time.deltaTime;
            }
            else
            {
                t -= Time.deltaTime;
            }
            
            float blend = Mathf.Clamp01(t / 0.5f);
            body.velocity = Vector2.up * Mathf.Lerp(1, -1, blend);

            if (t >= 0.5)
            {
                State = true;
            }
            else if(t <= 0)
            {
                State = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (life)
        {
            FindObjectOfType<AudioManager>().Play("hit");
            FindObjectOfType<AudioManager>().Play("Shineee");
            GameObject.Find("flashscreen").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            StartCoroutine(Flash());
        }

        life = false;
        if(col.gameObject.tag == "Pipe")
        {
            col.gameObject.GetComponent<Pipe_Controller>().DesativarColiders();
        }
        BirdFlapController.SetBool("IsDead", true);
        float Tamanho = 0;
        while(Tamanho < FindObjectOfType<AudioManager>().PlayingTime("Shineee"))
        {
            Tamanho += Time.deltaTime;
        }
        GameObject.Find("End Game").GetComponent<End_Game>().StartCor();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        points++;
        FindObjectOfType<AudioManager>().Play("Point");
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
        }

        GameObject.Find("Points").GetComponent<TMPro.TextMeshPro>().text = points.ToString();
    }

    IEnumerator Flash()
    {
        Color color = GameObject.Find("flashscreen").GetComponent<SpriteRenderer>().color;
        float t = 0;

        while (t < 1.5f)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / 1.5f);
            color.a = Mathf.Lerp(color.a, 0, blend);
            GameObject.Find("flashscreen").GetComponent<SpriteRenderer>().color = color;

            yield return null;
        }
    }

    IEnumerator Movement()
    {


        yield return null;
    }
}
