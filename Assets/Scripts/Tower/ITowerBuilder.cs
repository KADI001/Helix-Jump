using System;

public interface ITowerBuilder
{
    public event Action<Tower> TowerBuilt;
    public event Action<Platform[]> PlatformsBuilt;
}
