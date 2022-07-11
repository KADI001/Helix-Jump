using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerPrefabs : MonoBehaviour
{
    [SerializeField] private Ball _ballPrefab;

    public Ball Ball => _ballPrefab;
}
