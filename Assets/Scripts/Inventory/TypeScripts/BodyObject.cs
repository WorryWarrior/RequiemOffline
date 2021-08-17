using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// One of six item classes used for correct instantiation of an object.
/// E.g. you will not be able to use a body object as a potion after instantiation.
/// </summary>
[CreateAssetMenu(fileName = "New Body Object", menuName = "InventorySystem/Items/Body")]
public class BodyObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Body;
    }
}
