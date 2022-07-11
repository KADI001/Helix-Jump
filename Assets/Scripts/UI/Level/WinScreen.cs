using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private WinScreenContent _content;
    private IBallJumper _ballJumper;

    private void Start()
    {
        HideContent();
    }

    private void OnEnable()
    {
        if (Validate())
        {
            _ballJumper.Won += OnBallWon;
        }
    }

    private void OnDisable()
    {
        if(Validate())
        {
            _ballJumper.Won -= OnBallWon;
        }
    }

    private bool Validate()
    {
        return _ballJumper != null;
    }

    public void Init(IBallJumper ballJumper)
    {
        _ballJumper = ballJumper;

        OnEnable();
    }

    private void OnBallWon()
    {
        ShowContent();
    }

    private void ShowContent()
    {
        _content.gameObject.SetActive(true);
    }

    private void HideContent()
    {
        _content.gameObject.SetActive(false);
    }
}
