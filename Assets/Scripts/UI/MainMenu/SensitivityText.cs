using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SensitivityText : MonoBehaviour
{
    [SerializeField] private SensitivitySlider _sensitivitySlider;
    private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _textMeshPro.text = _sensitivitySlider.Value.ToString();
    }

    private void OnEnable()
    {
        _sensitivitySlider.ValueChanged += OnSliderValueChanged;
    }

    private void OnDisable()
    {
        _sensitivitySlider.ValueChanged -= OnSliderValueChanged;
    }

    private void OnSliderValueChanged(int newValue)
    {
        _textMeshPro.text = newValue.ToString();
    }
}
