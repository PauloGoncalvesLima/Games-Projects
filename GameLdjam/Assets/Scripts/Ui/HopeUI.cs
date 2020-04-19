using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HopeUI : MonoBehaviour
{
    int curHope;
    int maxHope = 10;
    private Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        curHope = 2;
        slider = this.GetComponent<Slider>();
    }

    void updateSlider() {
        slider.value = (float) curHope/ (float) maxHope;
    }

    public int getHope() {
        return curHope;
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
