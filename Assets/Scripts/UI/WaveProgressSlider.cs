using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveProgressSlider : MonoBehaviour
{
    public Slider Slider;
    public float LerpTime;
    [Range(2, 5)] public int LerpPow;

    private float _startValue;
    private float _endValue;
    private float _time;

    public void SetValue(float value)
    {
        this.enabled = true;
        _time = 0f;
        _startValue = Slider.value;
        _endValue = Mathf.Clamp(value, Slider.minValue, Slider.maxValue);
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time >= LerpTime)
        {
            Slider.value = _endValue;
            enabled = false;
            return;
        }

        float t = Transforms.SmoothStopX(_time / LerpTime, LerpPow);
        Slider.value = Transforms.Lerp(t, _startValue, _endValue);
    }
}
