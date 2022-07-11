using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : IInputKeyboard
{
    private int _sensitivity;

    public MobileInput(int sensetivity)
    {
        _sensitivity = sensetivity;
    }

    public int Sensitivity => _sensitivity;

    public float GetValue()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                return -touch.deltaPosition.x * _sensitivity;
            }
        }

        return 0;
    }
}
