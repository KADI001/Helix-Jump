using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInput : IInputKeyboard
{
    private int _sensitivity;

    public PCInput(int sensitivity)
    {
        _sensitivity = sensitivity;
    }

    public int Sensitivity => _sensitivity;

    public float GetValue()
    {
        return Input.GetAxis("Horizontal") * _sensitivity;
    }
}
