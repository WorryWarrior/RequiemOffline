using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class used to create the UI window with basic information about the item.
/// </summary>
public class ItemPopup : MonoBehaviour
{
    private Image window;
    [SerializeField] private TextMeshProUGUI itemName = null;
    [SerializeField] private Image itemSprite = null;
    [SerializeField] private TextMeshProUGUI potionText = null;
    [SerializeField] private TextMeshProUGUI conText = null;
    [SerializeField] private TextMeshProUGUI intText = null;
    [SerializeField] private TextMeshProUGUI mndText = null;

    private void Awake()
    {
        window = GetComponent<Image>();
    }
    private void Start()
    {
        SetComponentsInvisible();
    }
    /// <summary>
    /// Updates UI window's position according to slot's position.
    /// </summary>
    /// <param name="position">Slot position determining window's position.</param>
    public void UpdatePosition(Vector3 position, float xOffset, float yOffset)
    {
        var panel = window.GetComponent<RectTransform>();
        panel.anchoredPosition = new Vector3(position.x + xOffset, position.y + yOffset);
    }
    /// <summary>
    /// Makes UI window visible enabling different text components depending on the item displayed.
    /// </summary>
    public void SetComponentsVisible(ItemObject obj ,string _itemName, Sprite _itemSprite, float _conValue, float _intValue, float _mndValue)
    {
        window.enabled = true;
        itemName.enabled = true;
        itemSprite.enabled = true;
        if (obj.type != ItemType.Potion)
        {
            conText.enabled = true;
            intText.enabled = true;
            mndText.enabled = true;
            UpdateItemValues(_itemName, _itemSprite, _conValue, _intValue, _mndValue);
        }
        else
        {
            potionText.enabled = true;
            UpdatePotionValues(_itemName, _itemSprite, obj.attribute, obj.restoredValue);
        }
    }
    /// <summary>
    /// Makes UI window invisible.
    /// </summary>
    public void SetComponentsInvisible()
    {
        window.enabled = false;
        itemName.enabled = false;
        itemSprite.enabled = false;
        potionText.enabled = false;
        conText.enabled = false;
        intText.enabled = false;
        mndText.enabled = false;
    }
    /// <summary>
    /// Updates components' values according to the input values.
    /// </summary>
    public void UpdateItemValues(string _ItemName, Sprite _ItemSprite, float _ConValue, float _IntValue, float _MndValue)
    {
        itemName.text = _ItemName;
        itemSprite.sprite = _ItemSprite;
        if (_ConValue == 0)
            conText.enabled = false;
        else
            conText.text = "CON: " + _ConValue;
        if (_IntValue == 0)
           intText.enabled = false;
        else
             intText.text = "INT: " + _IntValue;
        if (_MndValue == 0)
             mndText.enabled = false;
        else
           mndText.text = "MND: " + _MndValue;
    }
    /// <summary>
    /// Updates components' values according to the input values.
    /// </summary>
    public void UpdatePotionValues(string _ItemName, Sprite _ItemSprite, RestoredAttribute whatToRestore, float restoredValue)
    {
        itemName.text = _ItemName;
        itemSprite.sprite = _ItemSprite;
        string restoredAttribute;
        if (whatToRestore == RestoredAttribute.Health) 
            restoredAttribute = " health.";
        else
            restoredAttribute = " mana.";
        potionText.text = "Restores " + restoredValue + restoredAttribute;
    }
}
