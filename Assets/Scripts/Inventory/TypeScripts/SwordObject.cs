using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// One of six item classes used for correct instantiation of an object.
/// See <see cref="BodyObject"/> class for usage example.
/// </summary>
[CreateAssetMenu(fileName = "New Sword Object", menuName = "InventorySystem/Items/Sword")]
public class SwordObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Sword;
    }
}
