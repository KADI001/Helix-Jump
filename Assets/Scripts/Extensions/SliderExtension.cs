using UnityEngine;
using UnityEngine.UI;

public static class SliderExtension
{
    public static void AddValueWithMaxClamp(this Slider slider, float delta, float maxValue)
    {
        slider.value += delta;

        if (slider.value > maxValue)
            slider.value = maxValue;
    }

    public static void AddValueWithLerp(this Slider slider, float targetValue, float step)
    {
        slider.value = Mathf.Lerp(slider.value, targetValue, step);
    }
}
