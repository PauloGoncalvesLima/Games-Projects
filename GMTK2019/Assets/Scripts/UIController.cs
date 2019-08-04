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
        StartCoroutine(ImageFade(true, blackScreen.GetComponent<Image>()));
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
        StartCoroutine(ImageFade(false, blackScreen.GetComponent<Image>()));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
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

    IEnumerator ImageFade(bool fadeAway, Image img)
    {
        Color color = img.color;
        float t = 0;

        // fade from opaque to transparent
        if (fadeAway)
        {
            
            while (t < 2f)
            {
                t += Time.deltaTime;
                float blend = Mathf.Clamp01(t / 2f);
                color.a = Mathf.Lerp(color.a, 0, blend);
                img.color = color;

                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            while (t < 2f)
            {
                t += Time.deltaTime;
                float blend = Mathf.Clamp01(t / 2f);
                color.a = Mathf.Lerp(color.a, 1, blend);
                img.color = color;

                yield return null;
            }
        }
    }
}
