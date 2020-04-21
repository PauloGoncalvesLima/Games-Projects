using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInfo : MonoBehaviour
{
    public enum Types {Dialogue,Battery,Hope,Mine,PassDialogue, Pickaxe, Conversation,ChangeableDialogue,End};
    public Types type;
    [HeaderAttribute("Meta Info")]
    public string dialogue;
    public string dialogueAlternative;
    public int hopeAmmount;
    public int hopeAmmountAlternative;
    public string[] ConversationPlayer;
    public string[] ConversationResponse;
    public bool isDestructibleDialog;
    public bool lastDialogIsFromPlayer;
    public bool firstDialogIsFromPlayer;
    public string boolToSet;
}
