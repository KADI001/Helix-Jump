using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameProgressText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private TextMeshProUGUI _nextLevelText;

    private int _currentLevel;

    public void Init(int currentLevel)
    {
        _currentLevel = currentLevel;

        UpdateTexts();
    }

    private void UpdateTexts()
    {
        _currentLevelText.text = _currentLevel.ToString();
        _nextLevelText.text = (_currentLevel + 1).ToString();
    }
}
