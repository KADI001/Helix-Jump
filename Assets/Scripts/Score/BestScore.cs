using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestScore : IDisposable
{
    private IBallJumper _ballJumper;
    private Score _score;
    private int _currentBestScore;

    public event Action<int> BestScoreChanged;

    public BestScore(IBallJumper ballJumper, Score score, int currentBestScore)
    {
        _ballJumper = ballJumper;
        _score = score;
        _currentBestScore = currentBestScore;

        OnEnable();
    }

    public int CurrentBestScore => _currentBestScore;

    public void OnEnable()
    {
        _ballJumper.Lost += OnBallFinished;
        _ballJumper.Won += OnBallFinished;
    }

    public void OnDisable()
    {
        _ballJumper.Lost -= OnBallFinished;
        _ballJumper.Won -= OnBallFinished;
    }

    private void OnBallFinished()
    {
        if(_score.Value > _currentBestScore)
        {
            BestScoreChanged?.Invoke(_score.Value);
        }
    }

    public void Dispose()
    {
        OnDisable();
    }
}
