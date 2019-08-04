using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class sceneController : MonoBehaviour
{
    UIController ui;
    public AudioSource creak;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ui = GameObject.Find("UI").GetComponent<UIController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Char"))
        {
            StartCoroutine(NextScene());
            gameObject.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    IEnumerator NextScene()
    {
        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(ui.ImageFade(false));
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            yield return null;
        }
    }

    public void Creak()
    {
        creak.Play();
    }
}
