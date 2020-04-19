using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {
    private float typingSpeed = 0.05f;
    
    [HeaderAttribute("TMPro")]
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private TextMeshProUGUI interactBox;
    private string sentence;
    private bool CoroutineRunning;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        CoroutineRunning = false;
    }
    public void showInteract() {
        interactBox.gameObject.SetActive(true);
    }

    public void hideInteract() {
        interactBox.gameObject.SetActive(false);
    }

    public IEnumerator startDialogue(string s) {
        sentence = s;
        yield return new WaitWhile(() => CoroutineRunning == true);
        StartCoroutine(typeDialogue());
        yield return new WaitForSeconds(4f);
        dialogueBox.text = "";
        CoroutineRunning = false;
    }

    private IEnumerator typeDialogue() {
        CoroutineRunning = true;
        foreach (char letter in sentence.ToCharArray()) {
            dialogueBox.text += letter;
            // play type writter sound
            yield return new WaitForSeconds(typingSpeed);
        }
        
    }
}