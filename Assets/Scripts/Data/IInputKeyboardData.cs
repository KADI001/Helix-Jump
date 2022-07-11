using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IInputKeyboardData
{
    public int Sensitivity { get; }
    public int MinValue { get; }
    public int MaxValue { get; }
}
