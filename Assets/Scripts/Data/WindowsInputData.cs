using System;

[Serializable]
public class WindowsInputData : IInputKeyboardData, IRecoverableData
{
    private int _sensitivity;

    public WindowsInputData()
    {
        _sensitivity = 200;
    }

    public WindowsInputData(int sensetivity)
    {
        _sensitivity = sensetivity;
    }

    public int Sensitivity => _sensitivity;
    public int MinValue => 100;
    public int MaxValue => 400;

    public static WindowsInputData Default => new WindowsInputData();

    public void RecoveryAsDefaultData(string key, IDataSaver saveData)
    {
        saveData.Save(Default, key);
    }
}
