using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : IDisposable
{
    private PauseManager _pauseManager;
    private BallJumper _ballJumper;
    private LevelProgress _levelProgress;

    public PauseHandler(PauseManager pauseManager, BallJumper ballJumper, LevelProgress levelProgress)
    {
        _pauseManager = pauseManager;
        _ballJumper = ballJumper;
        _levelProgress = levelProgress;

        OnEnable();
    }

    private void OnEnable()
    {
        if (Validate() == true)
        {
            _ballJumper.Lost += OnBallFinished;
            _ballJumper.Won += OnBallFinished;
        }
    }

    private void OnDisable()
    {
        if (Validate() == true)
        {
            _ballJumper.Lost -= OnBallFinished;
            _ballJumper.Won -= OnBallFinished;
        }
    }

    private bool Validate()
    {
        return _ballJumper != null;
    }

    private void OnBallFinished()
    {
        _pauseManager.Pause();
    }

    public void Dispose()
    {
        OnDisable();
    }
}
