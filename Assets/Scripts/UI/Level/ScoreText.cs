using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreText : MonoBehaviour
{
    private Score _score;
    private TextMeshProUGUI _textMeshPro;

    private void OnEnable()
    {
        if (_score != null)
            SubscribeOnPlatformDestroyerEvents();
    }

    private void OnDisable()
    {
        if (_score != null)
            UnSubscribeOnPlatformDestroyerEvents();
    }

    public void Init(Score score)
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _score = score;
        SubscribeOnPlatformDestroyerEvents();

        UpdateScoreText();
    }

    private void SubscribeOnPlatformDestroyerEvents()
    {
        _score.ValueChanged += OnScoreValueChanged;
    }

    private void UnSubscribeOnPlatformDestroyerEvents()
    {
        _score.ValueChanged -= OnScoreValueChanged;
    }

    private void OnScoreValueChanged()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _textMeshPro.text = _score.Value.ToString();
    }
}