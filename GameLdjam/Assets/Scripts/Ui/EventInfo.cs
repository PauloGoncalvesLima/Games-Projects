using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInfo : MonoBehaviour
{
    public enum Types {Dialogue,Battery,Hope,Mine,PassDialogue, Pickaxe};
    public Types type;
    [HeaderAttribute("Meta Info")]
    public string dialogue;
    public int hopeAmmount;
}
