using System;

public interface IBallJumper 
{
    public event Action Grounded;
    public event Action Lost;
    public event Action Won;
}
