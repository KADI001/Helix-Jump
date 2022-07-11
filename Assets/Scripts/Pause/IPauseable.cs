using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPauseable
{
    public bool IsPaused { get; }

    public event Action Paused;
    public event Action Resumed;

    void Pause();
    void Resume();
}
