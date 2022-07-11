using System;

public interface ISaveSytem : IDataSaver, IDataLoader
{
    public string Path { get; }
}

public interface IDataSaver
{
    public void Save<T>(T data, string fileName);
}

public interface IDataLoader
{
    public T Load<T>(string fileName);
}
