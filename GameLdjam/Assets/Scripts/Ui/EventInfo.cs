using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInfo : MonoBehaviour
{
    public enum Types {Dialogue,Battery,Hope,Mine,PassDialogue, Pickaxe, Conversation};
    public Types type;
    [HeaderAttribute("Meta Info")]
    public string dialogue;
    public int hopeAmmount;
    public string[] ConversationPlayer;
    public string[] ConversationResponse;
    public bool lastDialogIsFromPlayer;
    public bool firstDialogIsFromPlayer;
}
