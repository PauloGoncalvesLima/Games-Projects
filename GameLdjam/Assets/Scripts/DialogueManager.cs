using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {
    private float typingSpeed = 0.05f;
    
    [HeaderAttribute("TMPro")]
    [SerializeField] public TextMeshProUGUI dialogueBox;
    [SerializeField] public TextMeshProUGUI interactBox;
    private string sentence;
    private bool dbState;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Awake() {
        dbState = false;
    }
    public void showInteract() {
        interactBox.gameObject.SetActive(true);
    }

    public void hideInteract() {
        interactBox.gameObject.SetActive(false);
    }


    public void showDiag() {
        dbState = true;
        dialogueBox.gameObject.SetActive(dbState);
    }

    public void hideDiag() {
        dbState = false;
        dialogueBox.gameObject.SetActive(dbState);
    }
    public bool dbIsActive(){
        return dbState;
    }

    public IEnumerator startDialogue(string s) {
        sentence = s;
        yield return new WaitWhile(() => dbState);
        showDiag();
        StartCoroutine(typeDialogue());
        yield return new WaitForSeconds(4f);
    }

    private IEnumerator typeDialogue() {
        foreach (char letter in sentence.ToCharArray()) {
            dialogueBox.text += letter;
            // play type writter sound
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(2f);
        dialogueBox.text = "";
        hideDiag();
    }
}