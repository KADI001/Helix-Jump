using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilderPrefabs : MonoBehaviour
{
    [SerializeField] private Floor _floorPrefab;
    [SerializeField] private Pillar _pillarPrefab;
    [SerializeField] private Platform[] _platformPrefabs;
    [SerializeField] private StartPlatform _startPlatformPrefab;

    public Floor Floor => _floorPrefab;
    public Pillar Pillar => _pillarPrefab;
    public Platform[] Platforms => _platformPrefabs;
    public StartPlatform StartPlatform => _startPlatformPrefab;

    public int PlatformsAmount => Platforms.Length;

    public Platform GetRandomPlatform()
    {
        return Platforms[Random.Range(0, Platforms.Length)];
    }
}
