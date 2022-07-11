public interface IRecoverableData
{
    public abstract void RecoveryAsDefaultData(string key, IDataSaver saveData);
}
