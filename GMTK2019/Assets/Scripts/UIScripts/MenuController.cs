using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    Transform gameOver, restart, shadow, blackScreen;

    private void Start()
    {
        blackScreen = transform.GetChild(3);
        StartCoroutine(ImageFade(true));
    }

    public void RestartGame()
    {
        StartCoroutine(RestartGameCourotine());
    }

    public void NextScene()
    {
        StartCoroutine(NextSceneCourotine());
    }

    IEnumerator RestartGameCourotine()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            StartCoroutine(ImageFade(false));
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(0, LoadSceneMode.Single);
            yield return null;
        }
    }

    IEnumerator NextSceneCourotine()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            StartCoroutine(ImageFade(false));
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            yield return null;
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
