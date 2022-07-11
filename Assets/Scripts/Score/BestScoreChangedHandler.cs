using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestScoreChangedHandler : IDisposable
{
    private IDataSaver _saveSytem;
    private BestScore _bestScore;

    public BestScoreChangedHandler(IDataSaver saveSytem, BestScore bestScore)
    {
        _saveSytem = saveSytem;
        _bestScore = bestScore;

        OnEnable();
    }

    private void OnEnable()
    {
        _bestScore.BestScoreChanged += OnBestScoreChanged;
    }

    private void OnDisable()
    {
        _bestScore.BestScoreChanged -= OnBestScoreChanged;
    }

    private void OnBestScoreChanged(int newValue)
    {
        _saveSytem.Save(new BestScoreData(newValue), "BestScore");
    }

    public void Dispose()
    {
        OnDisable();
    }
}
