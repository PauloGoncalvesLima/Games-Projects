using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Game : MonoBehaviour
{


    public void StartCor()
    {
        
        StartCoroutine(GameOver());
        FindObjectOfType<AudioManager>().Play("swoosh");
    }


    IEnumerator GameOver()
    {
        Vector3 gameover = this.gameObject.transform.GetChild(0).gameObject.transform.position;
        Vector3 restart = this.gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>().position;

        float t = 0;

        while (t < 2f)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / 2f);
            gameover = Vector3.up * Mathf.Lerp(gameover.y,1f, blend);
            this.gameObject.transform.GetChild(0).gameObject.transform.position = gameover;

            restart = Vector3.up * Mathf.Lerp(restart.y, -1f, blend);
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>().position = restart;
            yield return null;
        }
    }
}
