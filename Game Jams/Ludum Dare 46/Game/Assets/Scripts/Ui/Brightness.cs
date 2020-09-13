using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;
public class Brightness : MonoBehaviour
{
    [SerializeField]
    
    [Tooltip("The text shown will be formatted using this string.  {0} is replaced with the actual value")]
    public GameObject GL;
    public GameObject Parent;
    public GameObject slider;
    private string formatText = "{0}%";
    [SerializeField] public TextMeshProUGUI tmproText;
    private void Start()
    {
        HandleValueChanged(slider.GetComponent<Slider>().value);
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
        if(Parent.GetComponent<Pause>().isPaused){
            HandleValueChanged(slider.GetComponent<Slider>().value);
        }
        
    }
    private void HandleValueChanged(float value)
    {
        tmproText.text = string.Format(formatText, Mathf.Floor(value*100));
        GL.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = SliderNumbers(value);
    }

    float SliderNumbers(float p) {
        if (p <= .25) {
            return 0.005f + p/5;
        } else {
            return 1.26f * p - 0.26f;
        }
    }
}