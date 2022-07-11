using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaveSystem : ISaveSytem
{
    private BinaryFormatter _binaryFormatter;
    private Dictionary<string, Type> _keyTypePair;

    private readonly string _path;
    private readonly string _extension = ".al";

    public BinarySaveSystem() 
    {
        _binaryFormatter = new BinaryFormatter();

        _keyTypePair = new Dictionary<string, Type>()
        {
            {"TowerConfig", typeof(TowerBuilderData) },
            {"GameProgress", typeof(GameProgressData) },
            {"BestScore", typeof(BestScoreData) },
            {"ScoreRewards", typeof(ScoreData) },
            {"AndroidController", typeof(AndroidInputData) },
            {"WindowsController", typeof(WindowsInputData) },
        };
        
        _path = Application.persistentDataPath + "\\";
    }

    public string Path => _path;

    public T Load<T>(string key)
    {
        string fullPath = GetFullPath(key);

        if (File.Exists(fullPath) == false)
        {
            return default;
        }

        using (Stream fileStream = File.OpenRead(fullPath))
        {
            return (T)_binaryFormatter.Deserialize(fileStream);
        }
    }

    public void Save<T>(T data, string key)
    {
        string fullPath = GetFullPath(key);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        using(Stream fileStream = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write))
        {
            _binaryFormatter.Serialize(fileStream, data);    
        }

        if(_keyTypePair.ContainsKey(key) == false)
        {
            _keyTypePair.Add(key, typeof(T));
        }
    }

    public bool TryRecoveryAllEmptyDataAsDefault()
    {
        int amountOfNotRecoveredData = 0;

        foreach (var pair in _keyTypePair)
        {
            string key = pair.Key;
            Type type = pair.Value;

            if (HasData(key) == false)
            {
                bool hasInterfaceIRecoverableData = type.GetInterface(nameof(IRecoverableData)) != null;

                if (hasInterfaceIRecoverableData == true)
                {
                    IRecoverableData data = (IRecoverableData)Activator.CreateInstance(type);
                    data.RecoveryAsDefaultData(key, this);
                }
                else
                {
                    amountOfNotRecoveredData++;
                }
            }
        }

        if (amountOfNotRecoveredData > 0)
            return false;
        else
            return true;
    }

    public bool HasData(string key) => File.Exists(GetFullPath(key)) == true;
    private string GetFullPath(string key) => _path + key + _extension;
}