using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUi : MonoBehaviour
{
    float curBattery;
    float maxBattery = 120f;
    bool isOn;
    public GameObject Light;
    private Slider slider;
    
    // Start is called before the first frame update
    void Awake() {
        curBattery = 60f;
        slider = this.GetComponent<Slider>();
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        // tick battery
        curBattery -= Time.deltaTime;
        if (curBattery < 0) {
            curBattery = 0;
        }
        float percent = LigthDecay(curBattery/maxBattery);
        handleOnOff(percent);
        slider.value = percent;
        Light.GetComponent<FlashLigth>().setPower(LightPower(percent));
    }

    void handleOnOff(float p) {
        if (isOn && p <= 0) {
            StartCoroutine(Light.GetComponent<FlashLigth>().TurnOffLight());
            isOn = false;
        } else if (!isOn && p >= 0) {
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
