using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// One of six item classes used for correct instantiation of an object.
/// See <see cref="BodyObject"/> class for usage example.
/// </summary>
[CreateAssetMenu(fileName = "New Boots Object", menuName = "InventorySystem/Items/Boots")]
public class BootsObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Boots;
    }
}
