using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerBuilderPrefabs))]
[RequireComponent(typeof(BallSpawnerPrefabs))]
public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private FollowingCamera _mainCamera;

    [Space(10)]
    [Header("UI components")]
    [SerializeField] private ProgressBarSlider _progressBar;
    [SerializeField] private ScoreText _scoreText;
    [SerializeField] private LoseScreen _loseScreen;
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private BestScoreText _bestScoreText;
    [SerializeField] private GameProgressText _gameProgressText;

    private TowerBuilderPrefabs _towerBuilderPrefabs;
    private TowerBuilder _towerBuilder;
    private BallSpawnerPrefabs _ballSpawnerPrefabs;
    private BallSpawner _ballSpawner;

    private Tower _tower;
    private Ball _ball;
    private Score _score;
    private BestScore _bestScore;
    private BestScoreChangedHandler _bestScoreChangedHandler;
    private LevelProgress _levelProgress;
    private LevelEndedHandler _levelCompletedHandler; 
    private IInputKeyboard _inputKeyboard;
    private PauseManager _pauseManager;
    private PauseHandler _pauseHandler;

    private BinarySaveSystem _saveSystem;

    private void Start()
    {
        Compose();
    }

    private void Compose()
    {
        _saveSystem = new BinarySaveSystem();
        _saveSystem.TryRecoveryAllEmptyDataAsDefault();

        GameProgressData gameProgressData = _saveSystem.Load<GameProgressData>("GameProgress");
        BestScoreData bestScoreData = _saveSystem.Load<BestScoreData>("BestScore");
        ScoreData scoreData = _saveSystem.Load<ScoreData>("ScoreRewards");
        TowerBuilderData towerBuilderData = _saveSystem.Load<TowerBuilderData>("TowerConfig");
        IInputKeyboardData inputKeyboardData;

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                {
                    inputKeyboardData = _saveSystem.Load<AndroidInputData>("AndroidController");
                    _inputKeyboard = new MobileInput(inputKeyboardData.Sensitivity);
                }
                break;
            case RuntimePlatform.WindowsEditor:
                {
                    inputKeyboardData = _saveSystem.Load<WindowsInputData>("WindowsController");
                    _inputKeyboard = new PCInput(inputKeyboardData.Sensitivity);
                }
                break;
        }

        _towerBuilderPrefabs = GetComponent<TowerBuilderPrefabs>();
        _ballSpawnerPrefabs = GetComponent<BallSpawnerPrefabs>();

        _towerBuilder = new TowerBuilder(towerBuilderData, _towerBuilderPrefabs);
        _ballSpawner = new BallSpawner(_ballSpawnerPrefabs);

        _tower = _towerBuilder.BuildTower(Vector3.zero);
        _ball = _ballSpawner.SpawnBall(_tower.SpawnPoint);
        _tower.TowerRotator.Init(_inputKeyboard);
        _mainCamera.Init(_ball, _tower.Pillar);

        _score = new Score(_towerBuilder.PlatformDestroyers, _ball.BallJumper, scoreData);
        _bestScore = new BestScore(_ball.BallJumper, _score, bestScoreData.BestScore);
        _bestScoreChangedHandler = new BestScoreChangedHandler(_saveSystem, _bestScore);
        _levelProgress = new LevelProgress(_ball.BallJumper, gameProgressData.Level);
        _levelCompletedHandler = new LevelEndedHandler(_saveSystem, _levelProgress);

        _pauseManager = new PauseManager();
        _pauseHandler = new PauseHandler(_pauseManager, _ball.BallJumper, _levelProgress);

        _pauseManager.Add(_tower.TowerRotator);
        _pauseManager.Add(_ball.BallJumper);

        _scoreText.Init(_score);
        _bestScoreText.Init(bestScoreData.BestScore);
        _progressBar.Init(_towerBuilder.PlatformDestroyers);
        _gameProgressText.Init(gameProgressData.Level);
        _loseScreen.Init(_ball.BallJumper);
        _winScreen.Init(_ball.BallJumper);
    }
}
