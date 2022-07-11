using System;

[Serializable]
public class GameProgressData : IRecoverableData
{
    public readonly int Level;

    public GameProgressData()
    {
        Level = 1;
    }

    public GameProgressData(int level)
    {
        Level = level;
    }

    public static GameProgressData Default => new GameProgressData();

    public void RecoveryAsDefaultData(string key, IDataSaver saveData)
    {
        saveData.Save(Default, key);
    }
}
