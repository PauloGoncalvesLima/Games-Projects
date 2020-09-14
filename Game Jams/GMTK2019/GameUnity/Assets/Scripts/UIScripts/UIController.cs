using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    Transform gameOver, restart, shadow, blackScreen;

    private void Start()
    {
        gameOver = transform.GetChild(0);
        restart = transform.GetChild(1);
        shadow = transform.GetChild(2);
        blackScreen = transform.GetChild(5);
        StartCoroutine(ImageFade(true));
    }

    void FixedUpdate()
    {
        if (charMoviment.life == false)
        {
            gameOver.gameObject.SetActive(true);
            restart.gameObject.SetActive(true);
            shadow.gameObject.SetActive(true);
            StartCoroutine(TextFade(false, gameOver.GetComponent<TMPro.TextMeshProUGUI>()));
            StartCoroutine(TextFade(false, restart.GetComponentInChildren<TMPro.TextMeshProUGUI>()));
        }
    }

    public void RestartScene()
    {
        StartCoroutine(RestartSceneCourotine());
    }

    IEnumerator RestartSceneCourotine()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            StartCoroutine(ImageFade(false));
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            yield return null;
        }
    }

    IEnumerator TextFade(bool fadeAway,  TMPro.TextMeshProUGUI text)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                text.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                text.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

    public IEnumerator ImageFade(bool fadeAway)
    {
        Color color = blackScreen.GetComponent<Image>().color;
        float t = 0;

        // fade from opaque to transparent
        if (fadeAway)
        {
            
            while (t < 2f)
            {
                t += Time.deltaTime;
                float blend = Mathf.Clamp01(t / 2f);
                color.a = Mathf.Lerp(color.a, 0, blend);
                blackScreen.GetComponent<Image>().color = color;

                if (color.a == 0) { blackScreen.GetComponent<Image>().gameObject.SetActive(false); }

                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            blackScreen.GetComponent<Image>().gameObject.SetActive(true);
            while (t < 2f)
            {
                t += Time.deltaTime;
                float blend = Mathf.Clamp01(t / 2f);
                color.a = Mathf.Lerp(color.a, 1, blend);
                blackScreen.GetComponent<Image>().color = color;

                yield return null;
            }
        }
    }
}
