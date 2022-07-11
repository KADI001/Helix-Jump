using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour, IPlatformDestoroyer
{
    private int _explosionForce = 10;
    private float _secondsBeforeDestroy = 0.5f;
    private PlatformSegment[] _segments;

    public event Action PlatformDestroyed;

    private void Awake()
    {
        _segments = GetComponentsInChildren<PlatformSegment>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Ball ball))
        {
            ScatterSegments();
            PlatformDestroyed?.Invoke();
        }
    }

    private void ScatterSegments()
    {
        foreach (var segment in _segments)
        {
            segment.FlyAway(_explosionForce);
        }

        Destroy(gameObject, _secondsBeforeDestroy);
    }
}
