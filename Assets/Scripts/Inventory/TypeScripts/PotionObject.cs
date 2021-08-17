using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// One of six item classes used for correct instantiation of an object.
/// See <see cref="BodyObject"/> class for usage example.
/// </summary>
[CreateAssetMenu(fileName = "New Potion Object", menuName = "InventorySystem/Items/Potion")]
public class PotionObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Potion;
    }
}
