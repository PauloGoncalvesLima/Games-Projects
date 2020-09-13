using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSensor : MonoBehaviour
{
    public float totalTimeOfPower;
    float currTimeOfPower;
    public bool isCharging, hasPower, soundIsPlaying;
    public GameObject mask;
    public GameObject Pause;
    public GameObject Door;
    public GameObject Light;
    public Sprite OpenDoor;
    public Sprite CloseDoor;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        isCharging = false;
        hasPower = false;
        currTimeOfPower = 0;
        mask = this.transform.GetChild(1).gameObject;
        audioManager = FindObjectOfType<AudioManager>();
        soundIsPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.GetComponent<Pause>().isPaused){
            if(!Light.GetComponent<FlashLigth>().GethitLightSensor()){
            // Debug.Log(Light.GetComponent<FlashLigth>().GethitLightSensor());
            isCharging = false;
            }
            if(!isCharging){
                currTimeOfPower -= Time.deltaTime;
                if(currTimeOfPower < 0){
                    currTimeOfPower = 0;
                }
                if (soundIsPlaying) {
                    audioManager.Stop("LightSensor");
                    soundIsPlaying = false;
                }
            } else {
                currTimeOfPower += Time.deltaTime;
                if(currTimeOfPower > totalTimeOfPower){
                    currTimeOfPower = totalTimeOfPower;
                }
                if (!soundIsPlaying) {
                    audioManager.Play("LightSensor");
                    soundIsPlaying = true;
                }
            }

            if(currTimeOfPower > 0) {
                if (Door.GetComponent<SpriteRenderer>().sprite == CloseDoor) {
                    audioManager.Play("DoorOpen");
                }
                Door.GetComponent<BoxCollider2D>().isTrigger = true;//Open door
                Door.GetComponent<SpriteRenderer>().sprite = OpenDoor;
            } else { 
                if (Door.GetComponent<SpriteRenderer>().sprite == OpenDoor) {
                    audioManager.Play("DoorClose");
                }
                Door.GetComponent<BoxCollider2D>().isTrigger = false; //Close door
                Door.GetComponent<SpriteRenderer>().sprite = CloseDoor;
            }
            
            mask.transform.localScale = new Vector3(mask.transform.localScale.x, power(currTimeOfPower/totalTimeOfPower), mask.transform.localScale.z);
            // Debug.Log(isCharging);
        }
        
    }


    float power(float p) {
        return Mathf.Pow(p, (3/2));
    }
}
