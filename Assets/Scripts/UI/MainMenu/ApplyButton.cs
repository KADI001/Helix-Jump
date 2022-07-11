using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ApplyButton : MonoBehaviour
{
    [SerializeField] private SensitivitySlider _sensitivitySlider;
    private Button _button;

    public event Action<int> Clicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnMouseDown);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnMouseDown);
    }

    private void OnMouseDown()
    {
        Clicked?.Invoke(_sensitivitySlider.Value);

        _sensitivitySlider.UpdataData();
        _sensitivitySlider.Reset();
    }
}
