using System;

[Serializable]
public class ScoreData : IRecoverableData
{
    public readonly int DefaultReward;
    public readonly int RewardPerPlatform;

    public ScoreData()
    {
        DefaultReward = 6;
        RewardPerPlatform = 5;
    }

    public ScoreData(int defaultReward, int rewardPerPlatform)
    {
        DefaultReward = defaultReward;
        RewardPerPlatform = rewardPerPlatform;
    }

    public static ScoreData Default => new ScoreData();

    public void RecoveryAsDefaultData(string key, IDataSaver saveData)
    {
        saveData.Save(Default, key);
    }
}
