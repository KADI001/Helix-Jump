using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBarSlider : MonoBehaviour
{
    [SerializeField] private float _fillingSpeed;
    [SerializeField] private float _minFillingSpeed;
    [SerializeField] private float _maxFillingSpeed;

    private Slider _slider;
    private IPlatformDestoroyer[] _platformDestroyers;
    private int _currentPlatformNumber;
    private int _platformsAmount;
    private float _targetSliderValue;

    private void Update()
    {
        if(_targetSliderValue != _slider.value)
        {
            _slider.AddValueWithLerp(_targetSliderValue, _fillingSpeed * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        if (_platformDestroyers != null)
            SubscribeOnPlatformDestroyerEvents();
    }

    private void OnDisable()
    {
        if (_platformDestroyers != null)
            UnSubscribeOnPlatformDestroyerEvents();
    }

    private void OnValidate()
    {
        _fillingSpeed = _fillingSpeed < _minFillingSpeed ? _minFillingSpeed : _fillingSpeed;
        _fillingSpeed = _fillingSpeed > _maxFillingSpeed ? _maxFillingSpeed : _fillingSpeed;
    }

    public void Init(IReadOnlyCollection<IPlatformDestoroyer> platformDestoroyers)
    {
        _slider = GetComponent<Slider>();
        _platformDestroyers = platformDestoroyers.ToArray();
        _currentPlatformNumber = 0;
        _platformsAmount = _platformDestroyers.Length;

        UpdateTargetValue();
        SubscribeOnPlatformDestroyerEvents();
    }

    private void SubscribeOnPlatformDestroyerEvents()
    {
        foreach (var platformDestroyer in _platformDestroyers)
        {
            platformDestroyer.PlatformDestroyed += OnPlatformDestroyed;
        }
    }

    private void UnSubscribeOnPlatformDestroyerEvents()
    {
        foreach (var platformDestroyer in _platformDestroyers)
        {
            platformDestroyer.PlatformDestroyed -= OnPlatformDestroyed;
        }
    }

    private void OnPlatformDestroyed()
    {
        _currentPlatformNumber++;

        UpdateTargetValue();
    }

    private void UpdateTargetValue()
    {
        _targetSliderValue = (float)_currentPlatformNumber / _platformsAmount;
    }
}
