using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : IPauseManagerRegistrator, IPauseManager, IPauseable, IDisposable
{
    public event Action Paused;
    public event Action Resumed;

    private List<IPauseable> _pauseableObjects = new List<IPauseable>();

    private bool _isPaused;

    public bool IsPaused => _isPaused;

    public void OnEnable()
    {
        foreach (var pauseable in _pauseableObjects)
        {
            pauseable.Paused += Pause;
            pauseable.Resumed += Resume;
        }
    }

    public void OnDisable()
    {
        foreach (var pauseable in _pauseableObjects)
        {
            pauseable.Paused -= Pause;
            pauseable.Resumed -= Resume;
        }
    }

    public void Add(IPauseable pauseable)
    {
        _pauseableObjects.Add(pauseable);
    }

    public void Remove(IPauseable pauseable)
    {
        _pauseableObjects.Remove(pauseable);
    }

    public void Pause()
    {
        _isPaused = true;

        foreach (var pauseable in _pauseableObjects)
        {
            pauseable.Pause();
        }

        Paused?.Invoke();
    }

    public void Resume()
    {
        _isPaused = false;

        foreach (var pauseable in _pauseableObjects)
        {
            pauseable.Resume();
        }

        Resumed?.Invoke();
    }

    public void Dispose()
    {
        OnDisable();
    }
}
