using System;

[Serializable]
public class AndroidInputData : IInputKeyboardData, IRecoverableData
{
    private int _sensitivity;


    public AndroidInputData()
    {
        _sensitivity = 5;
    }

    public AndroidInputData(int sensivity)
    {
        _sensitivity = sensivity;
    }

    public int Sensitivity => _sensitivity;
    public int MinValue => 2;
    public int MaxValue => 10;

    public static AndroidInputData Default => new AndroidInputData();

    public void RecoveryAsDefaultData(string key, IDataSaver saveData)
    {
        saveData.Save(Default, key);
    }
}
