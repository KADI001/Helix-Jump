using System;

[Serializable]
public class TowerBuilderData : IRecoverableData
{
    private int _numberPlatforms = 0;
    private float _distanceBetweenPlatforms = 0;

    [NonSerialized] public const int MaxPlatfomsAmount = 100;
    [NonSerialized] public const int MinPlatfomsAmount = 25;
    [NonSerialized] public const int MaxDistanceBetweenPlatforms = 4;
    [NonSerialized] public const int MinDistanceBetweenPlatforms = 2;

    public TowerBuilderData()
    {
        NumberPlatforms = 50;
        DistanceBetweenPlatforms = 2.5f;
    }

    public TowerBuilderData(int numberPlatforms, float distanceBetweenPlatforms)
    {
        NumberPlatforms = numberPlatforms;
        DistanceBetweenPlatforms = distanceBetweenPlatforms;
    }

    public int NumberPlatforms 
    {
        get => _numberPlatforms;
        private set
        {
            _numberPlatforms = value;

            if (_numberPlatforms < MinPlatfomsAmount)
                _numberPlatforms = MinPlatfomsAmount;

            if (_numberPlatforms > MaxPlatfomsAmount)
                _numberPlatforms = MaxPlatfomsAmount;
        }
    }

    public float DistanceBetweenPlatforms
    {
        get => _distanceBetweenPlatforms;
        private set
        {
            _distanceBetweenPlatforms = value;

            if (_distanceBetweenPlatforms < MinDistanceBetweenPlatforms)
                _distanceBetweenPlatforms = MinDistanceBetweenPlatforms;

            if (_distanceBetweenPlatforms > MaxDistanceBetweenPlatforms)
                _distanceBetweenPlatforms = MaxDistanceBetweenPlatforms;
        }
    }

    public static TowerBuilderData Default => new TowerBuilderData();
    public static TowerBuilderData WithRandomNumberPlatforms => new TowerBuilderData(UnityEngine.Random.Range(MinPlatfomsAmount, MaxPlatfomsAmount), 2);

    public void RecoveryAsDefaultData(string key, IDataSaver saveData)
    {
        saveData.Save(Default, key);
    }
}
