using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : IDisposable
{
    private IBallJumper _ball;
    private int _level;

    public event Action LevelCompleted;

    public LevelProgress(IBallJumper ball, int currentLevel)
    {
        _ball = ball;
        _level = currentLevel;

        OnEnable();
    }

    public int Level => _level;

    private void OnEnable()
    {
        _ball.Won += OnBallWon;
    }

    private void OnDisable()
    {
        _ball.Won -= OnBallWon;
    }

    private void OnBallWon()
    {
        _level++;

        LevelCompleted?.Invoke();
    }

    public void Dispose()
    {
        OnDisable();
    }
}
