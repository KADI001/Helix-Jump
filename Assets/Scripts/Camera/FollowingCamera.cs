using System;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private float _distanceFromBall;
    [SerializeField] private float _speedFollowingCamera;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _angel;

    private Ball _ball;
    private Pillar _pillar;
    private Vector3 _minBallPosition;
    private Vector3 _rotationAxis;

    public Vector3 BallPosition
    {
        get
        {
            if(_ball != null)
                return _ball.transform.position;
            else
                throw new NullReferenceException();
        }
    }
    public Vector3 PillarPosition
    {
        get
        {
            if (_pillar != null)
                return _pillar.transform.position;
            else
                throw new NullReferenceException();
        }
    }

    private void Update()
    {
        if (_ball == null || _pillar == null)
            return;
        
        if (BallPosition.y < _minBallPosition.y)
        {
            FollowBall();
            _minBallPosition = BallPosition;
        }
    }

    public void Init(Ball ball, Pillar pillar)
    {
        _minBallPosition = ball.transform.position;
        _ball = ball;
        _pillar = pillar;
    }

    private void FollowBall()
    {
        Vector3 ball = _ball.transform.position;
        Vector3 pillar = _pillar.transform.position;
        pillar.y = ball.y;

        Vector3 directionOffset = (pillar - ball).normalized;
        Vector3 targetPosition = (ball - directionOffset * _distanceFromBall) + Vector3.up * _offsetY;
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, _speedFollowingCamera);

        transform.position = newPosition;

        transform.LookAt(_ball.transform);
        Vector3 rotation = transform.eulerAngles;
        rotation.x = _angel;
        transform.eulerAngles = rotation;
    }
}
