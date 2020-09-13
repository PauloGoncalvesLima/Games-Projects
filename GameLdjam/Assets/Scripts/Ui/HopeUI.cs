using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HopeUI : MonoBehaviour
{
    float curHope;
    float maxHope = 10f;
    public float timeOfDecay;
    private Slider slider;
    public GameObject Pause;
    public GameObject Battery;
    public GameObject GameOver;
    public GameObject cameraMain;
    public bool isDead;
    
    // Start is called before the first frame update
    void Start()
    {
        curHope = 2f;
        slider = this.GetComponent<Slider>();
        timeOfDecay = 0.1f;
    }
    private void Update() {
        if(Battery.GetComponent<Slider>().value == 0 && !Pause.GetComponent<Pause>().isPaused && Battery.GetComponent<BatteryUi>().Player.GetComponent<PlayerController>().ConversationIsOver){
            curHope -= Time.deltaTime * timeOfDecay;
                if(curHope < 0){
                    curHope = 0;
                    GameOver.SetActive(true);
                    Battery.GetComponent<BatteryUi>().Player.transform.position = new Vector3(0, 71, 0);
                    Battery.GetComponent<BatteryUi>().Player.SetActive(false);
                    GameOver.transform.position = new Vector3 (cameraMain.transform.position.x, cameraMain.transform.position.y, 8f); // This line
                    Battery.SetActive(false);
                    this.gameObject.SetActive(false);
                }
            curHope = hopeDecay(curHope / maxHope);
            updateSlider();
        }
    }

    void updateSlider() {
        slider.value = curHope / maxHope;
    }

    public float getHope() {
        return curHope;
    }
    float hopeDecay(float p) {
        return Mathf.Pow(p, (3/2)) * 10;
    }

    public void addHope(int h) {
        if (curHope + h > maxHope) {
            curHope = maxHope;
        } else if (curHope + h < 0) {
            curHope = 0;
        } else {
            curHope += h;
        }
        updateSlider();
    }
}
