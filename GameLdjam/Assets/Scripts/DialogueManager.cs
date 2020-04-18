using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {
    private float typingSpeed = 0.05f;
    
    [HeaderAttribute("TMPro")]
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private TextMeshProUGUI interactBox;
    private string sentence;

    public void showInteract() {
        interactBox.gameObject.SetActive(true);
    }

    public void hideInteract() {
        interactBox.gameObject.SetActive(false);
    }

    public IEnumerator startDialogue(string s) {
        sentence = s;
        StartCoroutine(typeDialogue());
        yield return new WaitForSeconds(4f);
        dialogueBox.text = "";
    }

    private IEnumerator typeDialogue() {
        foreach (char letter in sentence.ToCharArray()) {
            dialogueBox.text += letter;
            // play type writter sound
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}