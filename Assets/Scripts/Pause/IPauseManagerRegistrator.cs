using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPauseManagerRegistrator
{
    void Add(IPauseable pauseable);
    void Remove(IPauseable pauseable);
}
