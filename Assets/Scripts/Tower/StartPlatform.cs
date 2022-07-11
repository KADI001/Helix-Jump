using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    public Vector3 SpawnPoint => _spawnPoint.position;
}
