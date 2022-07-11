using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TowerRotator : MonoBehaviour, IPauseable
{
    public event Action Paused;
    public event Action Resumed;

    private Rigidbody _rigidbody;
    private IInputKeyboard _input; 
    private bool _inited;

    public bool IsPaused { get; private set; }

    private void Update()
    {
        if (IsPaused == true || _inited == false)
            return;

        Vector3 deltaRotation = Vector3.up * _input.GetValue() * Time.deltaTime;
        Quaternion newRotation = Quaternion.Euler(_rigidbody.rotation.eulerAngles + deltaRotation);
        _rigidbody.MoveRotation(newRotation);
    }

    public void Init(IInputKeyboard input)
    {
        _input = input;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;

        _inited = true;
    }

    public void Pause()
    {
        IsPaused = true;

        Paused?.Invoke();
    }

    public void Resume()
    {
        IsPaused = false;

        Resumed?.Invoke();
    }
}
