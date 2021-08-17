using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface used to implement items' stat bonuses.
/// </summary>
public interface IModifier 
{
    void AddValue(ref int baseValue);
}
