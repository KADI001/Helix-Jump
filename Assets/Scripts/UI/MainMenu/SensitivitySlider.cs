using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SensitivitySlider : MonoBehaviour
{
    private Slider _slider;
    private IInputKeyboardData _inputKeyboardData;

    public int Value => (int)_slider.value;

    public event Action<int> ValueChanged;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

        UpdataData();

        _slider.minValue = _inputKeyboardData.MinValue;
        _slider.maxValue = _inputKeyboardData.MaxValue;
        _slider.value = _inputKeyboardData.Sensitivity;
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(float newValue)
    {
        ValueChanged?.Invoke((int)newValue);
    }

    public void Reset()
    {
        _slider.value = _inputKeyboardData.Sensitivity;
    }

    public void UpdataData()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                _inputKeyboardData = new BinarySaveSystem().Load<WindowsInputData>("WindowsController");
                break;

            case RuntimePlatform.Android:
                _inputKeyboardData = new BinarySaveSystem().Load<AndroidInputData>("AndroidController");
                break;

            default:
                throw new InvalidOperationException("This platform is not supported");
        }
    }
}
