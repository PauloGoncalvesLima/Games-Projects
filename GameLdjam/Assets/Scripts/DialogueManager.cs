using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {
    private float typingSpeed = 0.05f;
    
    [HeaderAttribute("TMPro")]
    [SerializeField] public TextMeshProUGUI dialogueBox, interactBox;
    [SerializeField] public TextMeshProUGUI responseDBox;
    // private string sentence;
    private bool dbState, rState;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Awake() {
        dbState = false;
        rState = false;
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


    public void showResponseDBox() {
        rState = true;
        responseDBox.gameObject.SetActive(rState);
    }

    public void hideResponseDBox() {
        rState = false;
        responseDBox.gameObject.SetActive(rState);
    }
    public bool dbIsActive(){
        return dbState;
    }
    public bool rStateIsActive(){
        return rState;
    }

    public IEnumerator startDialogue(string s) {
        // sentence = s;
        yield return new WaitWhile(() => dbState);
        showDiag();
        StartCoroutine(typeDialogue(s));
        yield return new WaitForSeconds(4f);
    }

    private IEnumerator typeDialogue(string sentence) {
        foreach (char letter in sentence.ToCharArray()) {
            dialogueBox.text += letter;
            // play type writter sound
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(2f);
        dialogueBox.text = "";
        if(dbState){
            hideDiag();
        }
    }


    private IEnumerator typeResponse(string sentence) {
        foreach (char letter in sentence.ToCharArray()) {
            responseDBox.text += letter;
            // play type writter sound
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(2f);
        responseDBox.text = "";
        if(rState){
            hideResponseDBox();
        }
    }

    public IEnumerator responseDialog(string s){
        yield return new WaitWhile(() => dbState);
        showResponseDBox();
        StartCoroutine(typeResponse(s));
        yield return new WaitForSeconds(4f);
    }
}