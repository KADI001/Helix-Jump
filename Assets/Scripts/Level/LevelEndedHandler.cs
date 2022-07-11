using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndedHandler : IDisposable
{
    private ISaveSytem _saveSystem;
    private LevelProgress _levelProgress;

    public LevelEndedHandler(ISaveSytem saveSystem, LevelProgress gameProgress)
    {
        _saveSystem = saveSystem;
        _levelProgress = gameProgress;

        OnEnable();
    }

    private void OnEnable()
    {
        _levelProgress.LevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        _levelProgress.LevelCompleted -= OnLevelCompleted;
    }

    private void OnLevelCompleted()
    {
        _saveSystem.Save(new GameProgressData(_levelProgress.Level), "GameProgress");
    }

    public void Dispose()
    {
        OnDisable();
    }
}
