using System;
using UnityEngine;

[RequireComponent(typeof(TowerRotator))]
public class Tower : MonoBehaviour
{
    private Pillar _pillar;
    private TowerRotator _towerRotator;

    public Pillar Pillar => _pillar;
    public TowerRotator TowerRotator => _towerRotator;
    public Vector3 SpawnPoint { get; private set; }

    private void Awake()
    {
        _pillar = GetComponentInChildren<Pillar>();
        _towerRotator = GetComponent<TowerRotator>();
        SpawnPoint = _pillar.GetComponentInChildren<StartPlatform>().SpawnPoint;
    }
}
