using System;

[Serializable]
public class BestScoreData : IRecoverableData
{
    public readonly int BestScore;

    public BestScoreData()
    {
        BestScore = 0;
    }

    public BestScoreData(int bestScore)
    {
        BestScore = bestScore;
    }

    public static BestScoreData Default => new BestScoreData();

    public void RecoveryAsDefaultData(string key, IDataSaver saveData)
    {
        saveData.Save(Default, key);
    }
}