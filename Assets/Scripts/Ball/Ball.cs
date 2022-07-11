using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallJumper))]
public class Ball : MonoBehaviour
{
    private BallJumper _ballJumper;

    public BallJumper BallJumper => _ballJumper;

    private void Awake()
    {
        _ballJumper = GetComponent<BallJumper>();
    }
}
