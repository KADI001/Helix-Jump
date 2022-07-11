using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallJumper : MonoBehaviour, IBallJumper, IPauseable
{
    public event Action Grounded;
    public event Action Lost;
    public event Action Won;
    public event Action Paused;
    public event Action Resumed;

    [SerializeField] [Min(0)] private int _jumpForce;
    [SerializeField] [Min(1)] private int _maxFallingSpeed;

    public const int MaxJumpForce = 5;
    private Rigidbody _rigidBody;
    private Vector3 _velocityBeforePause;

    public bool IsPaused { get; private set; }

    private void OnValidate()
    {
        _jumpForce = _jumpForce > MaxJumpForce ? MaxJumpForce : _jumpForce;
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(IsFalling())
        {
            LimitFallingSpeed();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsPaused == true)
            return;

        bool isPlatformSegment = collision.gameObject.TryGetComponent(out PlatformSegment segment);
        bool isCrashZone = collision.gameObject.TryGetComponent(out CrashZone crashZone);
        bool isWinZone = collision.gameObject.TryGetComponent(out WinZone winZone);

        if (isPlatformSegment == true && isCrashZone == false)
        {
            Jump();

            Grounded?.Invoke();
        }
        else if (isCrashZone == true)
        {
            Lost?.Invoke();
        } 
        else if(isWinZone == true)
        {
            Won?.Invoke();
        }
    }

    public void Pause()
    {
        IsPaused = true;
        _velocityBeforePause = _rigidBody.velocity;
        _rigidBody.isKinematic = true;

        Paused?.Invoke();
    }

    public void Resume()
    {
        IsPaused = false;
        _rigidBody.velocity = _velocityBeforePause;
        _rigidBody.isKinematic = false;

        Resumed?.Invoke();
    }

    private void Jump()
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void LimitFallingSpeed()
    {
        Vector3 velocity = _rigidBody.velocity;
        velocity.y = Mathf.Clamp(velocity.y, -_maxFallingSpeed, 0);
        _rigidBody.velocity = velocity;
    }

    private bool IsFalling() => _rigidBody.velocity.y < 0;
}
