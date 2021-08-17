using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Wrapper for item representation within drop window UI, used for instantiation.
/// </summary>
public class DropPrefab : MonoBehaviour
{
    public Image slot;
    public Image droppedItemImage;
    public TextMeshProUGUI droppedItemName;
    /// <summary>
    /// Determines the values of prefab's instance.
    /// </summary>
    /// <param name="_droppedItemImage">Displayed sprite of the item.</param>
    /// <param name="_droppedItemName">Displayed name of the item.</param>
    public void SetDroppedItemUI(Sprite _droppedItemImage, string _droppedItemName)
    {
        droppedItemImage.sprite = _droppedItemImage;
        droppedItemName.text = _droppedItemName;
    }
}
