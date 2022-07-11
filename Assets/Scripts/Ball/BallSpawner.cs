using UnityEngine;

public class BallSpawner
{
    private BallSpawnerPrefabs _prefabs;

    public BallSpawner(BallSpawnerPrefabs prefabs)
    {
        _prefabs = prefabs;
    }

    public Ball SpawnBall(Vector3 position)
    {
        Ball ball = Object.Instantiate(_prefabs.Ball, position, Quaternion.identity);
        ball.gameObject.name = "Ball";

        return ball;
    }
}