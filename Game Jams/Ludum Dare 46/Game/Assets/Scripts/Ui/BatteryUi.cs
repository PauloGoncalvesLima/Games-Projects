using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUi : MonoBehaviour
{
    float curBattery;
    float maxBattery = 120f;
    bool isOn;
    public GameObject Pause;
    public GameObject Light;
    public GameObject Player;
    public GameObject PlayerDM;
    public AudioManager audioManager;
    private Slider slider;
    
    // Start is called before the first frame update
    void Awake() {
        curBattery = 0;
        slider = this.GetComponent<Slider>();
        isOn = false;
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.GetComponent<Pause>().isPaused && Player.GetComponent<PlayerController>().ConversationIsOver){
            // tick battery
            curBattery -= Time.deltaTime;
            if (curBattery < 0) {
                curBattery = 0;
            }
            float percent = LigthDecay(curBattery/maxBattery);
            handleOnOff(percent);
            // Debug.Log(percent);
            // Debug.Log(curBattery);
            // Debug.Log(isOn);
            slider.value = percent;
            if(isOn){
                Light.GetComponent<FlashLigth>().setPower(LightPower(percent));
            }
        }
        
        
    }

    void handleOnOff(float p) {
        if (isOn && p <= 0) {
            string[] phrases = new string[] {"Oh no! My headlight is out!", "Crap, it’s gonna get dark now!", "Battery is over, I don’t like this."};
            audioManager.Play("LightOff");
            StartCoroutine(Light.GetComponent<FlashLigth>().TurnOffLight());
            if(!PlayerDM.activeSelf){
                PlayerDM.SetActive(true);
            }
            StartCoroutine(PlayerDM.GetComponent<DialogueManager>().startDialogue(phrases[Random.Range(0,phrases.Length)]));
            isOn = false;
        } else if (!isOn && p > 0) {
            audioManager.Play("LightOn");  
            StartCoroutine(Light.GetComponent<FlashLigth>().TurnOnLight());
            isOn = true;
        }
    }

    public void addBattery() {
        if (curBattery + maxBattery/2 > maxBattery) {
            curBattery = maxBattery;
        } else {
            curBattery += maxBattery/2;
        }
    }

    float LigthDecay(float p) {
        return Mathf.Pow(p, (3/2));
    }

    float LightPower(float p) {
        return 10f + 17f*p;
    }
}
