using System;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private LoseScreenContent _content;
    private IBallJumper _ballJumper;

    private void Start()
    {
        HideLoseMenu();
    }

    private void OnEnable()
    {
        TrySubscribeOnPauseManagerEvents();
    }

    private void OnDisable()
    {
        TryUnSubscribeOnPauseManagerEvents();
    }

    public void Init(IBallJumper ballJumper)
    {
        _ballJumper = ballJumper;

        TrySubscribeOnPauseManagerEvents();
    }

    private void TrySubscribeOnPauseManagerEvents()
    {
        if (Validate())
        {
            _ballJumper.Lost += OnBallCrashed;
        }
    }

    private void TryUnSubscribeOnPauseManagerEvents()
    {
        if (Validate())
        {
            _ballJumper.Lost -= OnBallCrashed;
        }
    }

    private bool Validate()
    {
        return _ballJumper != null;
    }

    private void OnBallCrashed()
    {
        ShowLoseMenu();
    }

    private void HideLoseMenu()
    {
        _content.gameObject.SetActive(false);
    }

    private void ShowLoseMenu()
    {
        _content.gameObject.SetActive(true);
    }
}
