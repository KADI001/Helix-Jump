using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Score : IDisposable
{
    private IPlatformDestoroyer[] _platformDestoroyers;
    private BallJumper _ballJumper;
    private int _numberPlatformsBeforeCollision;
    private int _value;
    private int _defaultReward;
    private int _rewardPerPlatform;

    public event Action ValueChanged;

    public Score(IReadOnlyCollection<IPlatformDestoroyer> platformDestoroyers, BallJumper ballJumper, ScoreData info)
    {
        _platformDestoroyers = platformDestoroyers.ToArray();
        _ballJumper = ballJumper;
        _value = 0;
        _defaultReward = info.DefaultReward;
        _rewardPerPlatform = info.RewardPerPlatform;

        OnEnable();
    }

    public int Value => _value;
    public int DefaultReward => _defaultReward;
    public int RewardPerPlatform => _rewardPerPlatform;

    public void OnEnable()
    {
        foreach (var platformDestroyer in _platformDestoroyers)
        {
            platformDestroyer.PlatformDestroyed += OnPlatformDestroyed;
        }

        _ballJumper.Grounded += OnBallGrounded;
    }

    public void OnDisable()
    {
        foreach (var platformDestroyer in _platformDestoroyers)
        {
            platformDestroyer.PlatformDestroyed -= OnPlatformDestroyed;
        }

        _ballJumper.Grounded -= OnBallGrounded;
    }

    private void OnBallGrounded()
    {
        _numberPlatformsBeforeCollision = 0;
    }

    private void OnPlatformDestroyed()
    {
        _value += _defaultReward + (_rewardPerPlatform * _numberPlatformsBeforeCollision);
        _numberPlatformsBeforeCollision++;

        ValueChanged?.Invoke();
    }

    public void Dispose()
    {
        OnDisable();
    }
}
