using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderChange : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderText;
    public float sliderValue;

    public void Start()
    {
        slider.value = PlayerPrefs.GetFloat("sliderValue", sliderValue);
    }

    void Update()
    {
        sliderText.text = slider.value.ToString("0.0");
    }

    public void SetSliderValue(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("sliderValue", sliderValue);
    }
}
