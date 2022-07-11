using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private SettingsScreenContent _content;
    [SerializeField] private ApplyButton _applyButton;
    [SerializeField] private CancelButton _cancelButton;
    private BinarySaveSystem _saveSystem;

    private void Awake()
    {
        _saveSystem = new BinarySaveSystem();
        _saveSystem.TryRecoveryAllEmptyDataAsDefault();
    }

    private void OnEnable()
    {
        _applyButton.Clicked += OnApplyButtonClicked;
        _cancelButton.Clicked += OnCancelButtonClicked;
    }

    private void OnDisable()
    {
        _applyButton.Clicked -= OnApplyButtonClicked;
        _cancelButton.Clicked -= OnCancelButtonClicked;
    }

    public void Show()
    {
        _content.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _content.gameObject.SetActive(false);
    }

    private void OnApplyButtonClicked(int newSensetivity)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                _saveSystem.Save(new WindowsInputData(newSensetivity), "WindowsController");
                break;
            case RuntimePlatform.Android:
                _saveSystem.Save(new AndroidInputData(newSensetivity), "AndroidController");
                break;
        }

        Hide();
    }

    private void OnCancelButtonClicked()
    {
        Hide();
    }
}
