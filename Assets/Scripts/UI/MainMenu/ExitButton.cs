using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExitButton : MonoBehaviour
{
    private Button _button;

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
        Application.Quit();
    }
}
