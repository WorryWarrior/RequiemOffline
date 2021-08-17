using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object used to store each item's unique ID.
/// </summary>
[CreateAssetMenu(fileName = "New Item Database", menuName = "InventorySystem/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] itemObjects;
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < itemObjects.Length; i++)
        {
            itemObjects[i].data.Id = i;
        }
    }

    public void OnBeforeSerialize()
    { 
    }
}
