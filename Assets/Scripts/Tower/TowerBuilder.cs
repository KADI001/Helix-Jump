using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder
{
    private TowerBuilderData _config;
    private TowerBuilderPrefabs _prefabs;
    private Platform[] _platforms;
    private IPlatformDestoroyer[] _platformDestoroyers;

    public TowerBuilder(TowerBuilderData config, TowerBuilderPrefabs prefabs)
    {
        _config = config;
        _prefabs = prefabs;
    }

    public Vector3 SpawnPoint { get; private set; }
    public IReadOnlyCollection<IPlatformDestoroyer> PlatformDestroyers => _platformDestoroyers;

    public Tower BuildTower(Vector3 position)
    {
        Floor floor = BuildFloor(position);
        Pillar pillar = BuildPillar(floor);
        _platforms = BuildPlatforms(floor);
        StartPlatform startPlatform = BuildStartPlatform(floor);

        SpawnPoint = startPlatform.SpawnPoint;

        GameObject tower = new GameObject("Tower");

        pillar.transform.SetParent(tower);
        startPlatform.transform.SetParent(pillar);

        foreach (var platform in _platforms)
        {
            platform.transform.SetParent(pillar);
        }

        Tower towerComponent = tower.AddComponent<Tower>();
        tower.AddComponent<TowerRotator>();

        return towerComponent;
    }

    private Floor BuildFloor(Vector3 position)
    {
        return Object.Instantiate(_prefabs.Floor, position, Quaternion.identity);
    }

    private Platform[] BuildPlatforms(Floor floor)
    {
        int lenght = _prefabs.PlatformsAmount;
        _platforms = new Platform[_config.NumberPlatforms];
        _platformDestoroyers = new IPlatformDestoroyer[_config.NumberPlatforms];
        Vector3 startPosition = floor.transform.position + Vector3.up * 0.5f;

        for (int platformNumber = 1; platformNumber <= _config.NumberPlatforms; platformNumber++)
        {
            Vector3 platformPosition = startPosition + Vector3.up * platformNumber * _config.DistanceBetweenPlatforms;
            Quaternion angel = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 271), 0));
            Platform platform = Object.Instantiate(_prefabs.GetRandomPlatform(), platformPosition, angel);
            platform.gameObject.name = "Platform" + platformNumber;

            int index = platformNumber - 1;
            _platforms[index] = platform;
            _platformDestoroyers[index] = platform.PlatformDestroyer;
        }

        return _platforms;
    }

    private StartPlatform BuildStartPlatform(Floor floor)
    {
        int platfomsAmount = _config.NumberPlatforms;
        float distanceBetweenPlatforms = _config.DistanceBetweenPlatforms;
        Vector3 startPosition = floor.transform.position + Vector3.up * 0.5f;
        Vector3 startPlatformPosition = startPosition + (Vector3.up * (platfomsAmount * distanceBetweenPlatforms + distanceBetweenPlatforms));

        return Object.Instantiate(_prefabs.StartPlatform, startPlatformPosition, Quaternion.identity);
    }

    private Pillar BuildPillar(Floor floor)
    {
        int platfomsAmount = _config.NumberPlatforms;
        float distanceBetweenPlatforms = _config.DistanceBetweenPlatforms;
        Vector2 part1 = floor.transform.position + (Vector3.up * (platfomsAmount * distanceBetweenPlatforms * 0.5f + 5));
        Vector2 part2 = Vector3.up * _prefabs.Pillar.transform.localScale.y * 0.5f;
        Vector2 pillarPosition = part1 + part2;
        Vector3 oldScale = _prefabs.Pillar.transform.localScale;
        Pillar pillar = Object.Instantiate(_prefabs.Pillar, pillarPosition, Quaternion.identity);
        pillar.transform.localScale = new Vector3(oldScale.x, platfomsAmount * distanceBetweenPlatforms * 0.5f + 5, oldScale.z);

        return pillar;
    }
}